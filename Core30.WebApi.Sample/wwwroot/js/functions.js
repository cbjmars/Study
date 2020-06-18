const uri = "api/TbTestApis";
let ListItem = [];

function getItems() {
    $.ajax({
        type: "GET",
        url: uri,
        data: { action: "select" },
        success: function (data) {
            _displayItems(data);
        }
    });
}

// 리스트 바인딩
function _displayItems(data) {
    const button = document.createElement("button");
    const tBody = document.getElementById("items");

    _displayCount(data.length);
    if (data.length > 0) {
        tBody.innerHTML = "";
    }

    data.forEach(item => {
        let editButton = button.cloneNode(false);
        editButton.innerText = "Edit";
        editButton.setAttribute("onclick", "displayEditForm(" + item.seq + ")");

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = "Delete";
        deleteButton.setAttribute("onclick", "deleteItem(" + item.seq + ")");

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode1 = document.createTextNode(item.subject);
        td1.appendChild(textNode1);

        let td2 = tr.insertCell(1);
        let textNode2 = document.createTextNode(item.content);
        td2.appendChild(textNode2);

        let td3 = tr.insertCell(2);
        let textNode3 = document.createTextNode(item.greateDt);
        td3.appendChild(textNode3);

        let td4 = tr.insertCell(3);
        td4.appendChild(editButton);

        let td5 = tr.insertCell(4);
        td5.appendChild(deleteButton);
    });

    ListItem = data;
}

// 데이터 카운터
function _displayCount(itemCount) {
    const name = (itemCount === 1) ? "to-do" : "to-dos";

    document.getElementById("counter").innerText = itemCount + " " + name;
}

// 데이터 추가(저장)
function addItem() {
    let letSubject = document.getElementById("txtSubject_a");
    let letContent = document.getElementById("txtContent_a");
    let letGreateDt = document.getElementById("hddGreateDt_a");

    const item = {
        subject: letSubject.value,
        content: letContent.value,
        greateDt: letGreateDt.value
    };

    $.ajax({
        type: "POST",
        url: uri,
        data: JSON.stringify(item),
        contentType: "application/json;charset=utf-8",
        dataType: "text",
        async: false,
        cache: false,
        success: function (result) {
            if (result !== undefined) {
                getItems();
                letSubject.value = "";
                letContent.value = "";
            }
        },
        error: function (error) {
            alert(error.status + " : " + error.statusText);
        }
    });
}

// 데이터 수정
function displayEditForm(id) {
    const item = ListItem.find(item => item.seq === id);

    let letSubject = document.getElementById("txtSubject_m");
    let letContent = document.getElementById("txtContent_m");
    let letGreateDt = document.getElementById("hddGreateDt_m");
    let letSeq = document.getElementById("hddSeq");


    letSubject.value = item.subject;
    letContent.value = item.content;
    letGreateDt.value = item.greateDt;
    letSeq.value = item.seq;

    document.getElementById("inputForm").style.display = "none";
    document.getElementById("editForm").style.display = "";

}

function updateItem() {
    let letSeq = document.getElementById("hddSeq");
    let letSubject = document.getElementById("txtSubject_m");
    let letContent = document.getElementById("txtContent_m");
    let letGreateDt = document.getElementById("hddGreateDt_m");

    const item = {
        seq: parseInt(letSeq.value),
        subject: letSubject.value,
        content: letContent.value,
        greateDt: letGreateDt.value
    };

    $.ajax({
        type: "PUT",
        url: uri + "/" + item.seq,
        data: JSON.stringify(item),
        headers: { 'Accept': 'application/json' },
        contentType: "application/json;charset=utf-8",
        dataType: "text",
        async: false,
        cache: false,
        complete: function (resp) {
            if (resp.status === 204) { //204일 경우 NoContent 이므로.
                getItems();
                letSubject.value = "";
                letContent.value = "";
                //letGreateDt.value = "";
            }
        },
        error: function (error) {
            console.log(error);
            alert(error.status + " : " + error.statusText);
        }
    });

    document.getElementById("inputForm").style.display = "";
    document.getElementById("editForm").style.display = "none";
}

// 데이터 삭제
function deleteItem(id) {
    $.ajax({
        type: "DELETE",
        url: uri + "/" + id,
        dataType: "text",
        success: function (result) {
            if (result !== undefined) {
                getItems();
            }
        },
        error: function (error) {
            alert(error.status + " : " + error.statusText);
        }
    });
}