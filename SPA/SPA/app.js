let currentList = {};

function createShoppingList() {
    currentList.name = $("#shoppingListName").val();
    currentList.items = new Array();

    //web server call
    $.ajax({
        type: "POST",
        url: "api/ShoppingList",
        dataType:"json",
        data:currentList,
        success: function (result) {
            showShoppingList();
        }
    });

    //check
    //showShoppingList();
}

function showShoppingList() {
    //shoppingListTitle
    $("#shoppingListTitle").html(currentList.name);

    //shoppingListItem
    $("#shoppingListItem").empty();

    //createList
    $("#createList").hide();

    //shoppingListDiv
    $("#shoppingListDiv").show();

    //focus and enter key
    $("#newItemName").focus();
    $("#newItemName").keydown(function (events) {
        if (events.keyCode == 13) {
            console.log("enter key pressed.");
            addItem();
        }
    });

}

function addItem() {
    var newitem = {};
    newitem.name = $("#newItemName").val();
    newitem.shopingListId = currentList.id;

    /*   currentList.items.push(newitem);*/
    $.ajax({
        type: "POST",
        url: "api/Item",
        dataType: "json",
        data: newitem,
        success: function (result) {
            currentList = result;
            drawItems();
            $("#newItemName").val("");
        }
    });

    console.info(currentList);

    
}

function drawItems() {
    var $list = $("#shoppingListItem").empty();
    for (var i = 0; i < currentList.items.length; i++) {
        var currentitem = currentList.items[i];
        var $li = $("<li>").html(currentitem.name)
            .attr("id", "item_" + i);
        var $deleteBtn = $("<button onclick='deleteItem(" + currentitem.id + ")'> D </button>").appendTo($li);
        var $checkBtn = $("<button onclick='checkItem(" + currentitem.id + ")'> C </button>").appendTo($li);

        if (currentitem.isChecked) {
            $li.addClass("checked")
        } 
        $li.appendTo($list);

    }
}


function deleteItem(index) {

    $.ajax({
        url: "api/Item/" + index,
        type: "Delete",
        dataType: "json",
        success: function (result) {
            currentList = result;
            drawItems();

        }
    });

    //currentList.items.splice(index, 1);
}

function checkItem(index) {
    let item = currentList.items.find((element) => element.id == index);
    item.isChecked = !item.isChecked;
    $.ajax({
        type: "PUT",
        url: "api/Item/" + index,
        dataType: "json",
        data: item,
        success: function (result) {
            currentList = result;
            drawItems();
        }

    });
    
}


function getShoppingListByID(id) {
    console.info(id)

    $.ajax({
        type: "GET",
        data: "json",
        url: "api/ShoppingList/" + id,
        success: function (result) {
            currentList = result;
            showShoppingList();
            drawItems();
        }
    });


    //currentList.name = "Mock shopping List";
    //currentList.items = [
    //    { name: "Milk" },
    //    { name: "CornFlakes" },
    //    { name: "Strawberries" },
    //    { name: "biscuits" }
    //];

    //showShoppingList();
    //drawItems();
}


$(document).ready(function () {
    console.info("ready");

    $("#shoppingListName").focus();
    $("#shoppingListName").keydown(function (events) {
        if (events.keyCode == 13) {
            console.log("enter key pressed.");
            createShoppingList();
        }
    });


    var pageUrl = window.location.href;
    var idIndex = pageUrl.indexOf("?id=");

    if (idIndex != -1) {
        getShoppingListByID(pageUrl.substring(idIndex + 4));
    }
});