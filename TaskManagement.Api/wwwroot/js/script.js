const apiUrl = '/api/tasks';

document.addEventListener('DOMContentLoaded', loadTasks);

async function loadTasks() {
    const response = await fetch(apiUrl);
    const tasks = await response.json();
    const taskList = document.getElementById('task-list');
    const taskTable = document.querySelector('.task-table');
    taskList.innerHTML = '';

    if (tasks.length === 0) {
        taskTable.style.display = 'none';
    } else {
        taskTable.style.display = '';
        tasks.forEach(task => {
            const taskRow = document.createElement('tr');
            taskRow.className = 'task-item';
            taskRow.setAttribute('data-task-id', task.id); // Establecer el atributo data-task-id
            taskRow.innerHTML = `
                <td>${task.title}</td>
                <td>${task.description}</td>
                <td>${task.priority}</td>
                <td>${task.dueDate}</td>
                <td>${task.status}</td>
                <td>
                    <button onclick="editTask(${task.id})">Edit</button>
                    <button onclick="deleteTask(${task.id})">Delete</button>
                </td>
            `;
            taskList.appendChild(taskRow);
        });
    }
}

async function addTask() {
    const title = document.getElementById('task-title').value;
    const description = document.getElementById('task-description').value;
    const priority = document.getElementById('task-priority').value;
    const dueDate = document.getElementById('task-dueDate').value;

    const response = await fetch(apiUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            title,
            description,
            priority,
            dueDate,
            status: 'Open'
        })
    });

    if (response.ok) {
        loadTasks();
        document.getElementById('task-title').value = '';
        document.getElementById('task-description').value = '';
        document.getElementById('task-priority').value = '';
        document.getElementById('task-dueDate').value = '';
    } else {
        alert('Failed to add task.');
    }
}

async function editTask(id) {
    const taskRow = document.querySelector(`tr[data-task-id='${id}']`);
    if (!taskRow) {
        console.error(`No task row found with id ${id}`);
        return;
    }
    const cells = taskRow.children;

    cells[0].innerHTML = `<input type="text" value="${cells[0].textContent}" id="edit-title-${id}">`;
    cells[1].innerHTML = `<input type="text" value="${cells[1].textContent}" id="edit-description-${id}">`;
    cells[2].innerHTML = `<input type="number" value="${cells[2].textContent}" id="edit-priority-${id}">`;
    cells[3].innerHTML = `<input type="date" value="${cells[3].textContent}" id="edit-dueDate-${id}">`;
    cells[4].innerHTML = `<input type="text" value="${cells[4].textContent}" id="edit-status-${id}">`;

    const saveButton = document.createElement('button');
    saveButton.textContent = 'Save';
    saveButton.onclick = () => saveTask(id);
    cells[5].innerHTML = '';
    cells[5].appendChild(saveButton);
}

async function saveTask(id) {
    const newTitle = document.getElementById(`edit-title-${id}`).value;
    const newDescription = document.getElementById(`edit-description-${id}`).value;
    const newPriority = document.getElementById(`edit-priority-${id}`).value;
    const newDueDate = document.getElementById(`edit-dueDate-${id}`).value;
    const newStatus = document.getElementById(`edit-status-${id}`).value;

    const response = await fetch(`${apiUrl}/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            id,
            title: newTitle,
            description: newDescription,
            priority: newPriority,
            dueDate: newDueDate,
            status: newStatus
        })
    });

    if (response.ok) {
        loadTasks();
    } else {
        alert('Failed to update task.');
    }
}

async function deleteTask(id) {
    const response = await fetch(`${apiUrl}/${id}`, {
        method: 'DELETE'
    });

    if (response.ok) {
        loadTasks();
    } else {
        alert('Failed to delete task.');
    }
}
