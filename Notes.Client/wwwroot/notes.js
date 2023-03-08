function loadNotes() {
    // Loads all notes to the page
    fetch("http://localhost:5001/notes", {
        method: "GET",
        headers: { "Accept": "application/json"}
    }).then(response => response.json())
        .then(data => {
            const area = document.getElementById("for-posts");
            
            if (data.length === 0) {
                fetch(`http://localhost:5001/notes?name=Note&content=Text`,
                    {
                        method: "POST",
                        headers: { "Accept": "application/json"}
                    }).then(_ => location.reload())
                    .catch(err => {
                        console.log(err);
                    });
            }
            
            data.forEach(note => area.innerHTML += `
<section class="content__section">
<h2 id="notename">${note.name}</h2>
<p id="notecontent">${note.content}</p>
<p class="id" id="noteid">Id: ${note.id}</p>

</section>
`)})
        .catch(err => {
            console.log(err);
        });
}

function putNote() {
    // Updates node with specified ID with entered title and content
    let formElement = document.querySelector('.put__form');
    console.log(formElement);
    formElement.addEventListener('submit', e => {
        e.preventDefault();
        
        let id = document.getElementById('put-id').value;
        let name = document.getElementById('put-name').value;
        if (name === "") {
            name = "Unnamed";
        }
        
        let content = document.getElementById('put-content').value;
        
        fetch(`http://localhost:5001/notes?id=${id}&name=${name}&content=${content}`,
            {
                method: "PUT",
                headers: {"Accept": "application/json"}
            })
            .then(_ => location.reload())
            .catch(err => {
                console.log(err);
            });
    })
}

function deleteNote() {
    // Deletes note with specified ID
    let formElement = document.querySelector('.delete__form');
    formElement.addEventListener('submit', e => {
        e.preventDefault();
        
        let id = document.getElementById('note-id').value;
        
        fetch(`http://localhost:5001/notes?id=` + id,
            {
                method: "DELETE",
                headers: {"Accept": "application/json"}
            })
            .then(_ => location.reload())
            .catch(err => {
                console.log(err);
            });
    })
}

function addNote() {
    // Adds new node with entered title and content
    let formElement = document.querySelector('.add__form');
    formElement.addEventListener('submit', e => {
        e.preventDefault();

        let name = document.getElementById('note-name').value;
        if (name === "") {
            name = "Unnamed";
        }
        
        let content = document.getElementById('note-content').value;

        fetch(`http://localhost:5001/notes?name=` + name + `&content=` + content,
            {
                method: "POST",
                headers: { "Accept": "application/json"}
            }).then(_ => location.reload())
            .catch(err => {
                console.log(err);
            });
    })
}

function editNote() {
    // Shows the form to edit note with specified ID
    let formElement = document.querySelector('.edit__form');
    formElement.addEventListener('submit', e => {
        e.preventDefault();
        
        let id = document.getElementById('note-id-edit').value;
        
        fetch("http://localhost:5001/note-by-id?id=" + id, {
            method: "GET",
            headers: { "Accept": "application/json"}
        }).then(response => response.json())
            .then(data => {
                let name = data.name;
                let content = data.content;
                
                let editingElement = `
        <div class="content__section">
            <h2>Edit note</h2>
            <form class="put__form">
                <label for="put-name">Title</label>
                <input id="put-name" type="text" name="put-name" size="50" value="${name}">
                <label for="put-content">Content</label>
                <textarea id="put-content" name="put-content" rows="10">${content}</textarea>
                <label for="put-id">Id</label>
                <input id="put-id" type="text" name="put-id" value="${id}">
                <input id="put-button" type="submit" value="Edit">
            </form>
        </div>
        `;
                
                document.getElementById('edit').insertAdjacentHTML("afterend", editingElement);
                let putButton = document.getElementById("put-button");
                putButton.addEventListener('click', putNote);
            })
            .catch(err => {
                console.log(err);
            });
    })
}

document.addEventListener('DOMContentLoaded', addNote);
document.addEventListener('DOMContentLoaded', loadNotes);
document.addEventListener('DOMContentLoaded', deleteNote);
document.addEventListener('DOMContentLoaded', editNote);