function AddItem(form) {
    let ItemList = [];
    let JSONSerializedList;

    if (localStorage.getItem("JSONList") !== null) {
        JSONSerializedList = localStorage.getItem('JSONList');
        ItemList = JSON.parse(JSONSerializedList);
    }

    let NewItem = { 'ItemCode': form.ItemCode.value, 'Quantity': form.Quantity.value };
    console.log(NewItem);

    ItemList.push(NewItem);
    JSONSerializedList = JSON.stringify(ItemList);
    localStorage.setItem("JSONList", JSONSerializedList);

    Display();

    form.ItemCode.value = '';
    form.Quantity.value = '';
    form.ItemCode.focus();
    form.ItemCode.select();

    window.document.getElementById("SaleString").value = JSONSerializedList;
}

function Display() {
    let ItemList = [];
    let index = 0;
    let JSONSerializedList;
    let displayHTML = '<tr><th>Item Name</th><th>Quantity</th></tr>';

    if (localStorage.getItem("JSONList") !== null) {
        JSONSerializedList = localStorage.getItem("JSONList");
        ItemList = JSON.parse(JSONSerializedList);
    }
    if (ItemList.length > 0) {
        for (index = 0; index <= ItemList.length - 1; index++) {
            displayHTML += '<tr><td>' + ItemList[index].ItemCode + '</td> <td>'
                + ItemList[index].Quantity + '</td>'
                + '<td><input type="button" value="Remove" class="btn btn-danger" onclick="RemoveItem(' + index + ');" /> </td></tr>';
        }
    }
    else {
        displayHTML += '<tr><td>No Items</td></tr>';
    }

    window.document.getElementById("DisplayTable").innerHTML = displayHTML;
    window.document.getElementById("SaleString").value = JSONSerializedList;
}

function Clear() {
    localStorage.removeItem("JSONList");
    Display();
}

function RemoveItem(index) {
    let ItemList = [];
    let JSONSerializedList;
    console.log(index);
    if (localStorage.getItem("JSONList") !== null) {
        JSONSerializedList = localStorage.getItem('JSONList');
        ItemList = JSON.parse(JSONSerializedList);
    }
    ItemList.splice(index, 1);
    JSONSerializedList = JSON.stringify(ItemList);
    localStorage.setItem("JSONList", JSONSerializedList);
    Display();
}