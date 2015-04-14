$(function () {
    $("#grid").jqGrid({
        url: "/Home/GetFizzBuzzLists",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['Id', 'Number', 'Message', 'DateTime Entered', 'Active'],
        colModel: [
            { key: true, hidden: true,  name: 'Id', index: 'Id', editable: true },
            { key: false, name: 'Number', index: 'Number', editable: true },
            { key: false, name: 'Message', index: 'Message', editable: true },
            { key: false, hidden: true, name: 'DateTimeEntered', index: 'DateTimeEntered', editable: true, formatter: 'date', formatoptions: { newformat: 'd/m/Y' } },
            { key: false, hidden: true, name: 'Active', index: 'Active', editable: true, },
        ],
        pager: jQuery('#pager'),
        height: '500px',
        rowNum: 1000000,
        //rowList: [10, 20, 30, 40],
        shrinktofit: true,
        viewrecords: true,
        caption: 'Todo List',
        emptyrecords: 'No records to display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        autowidth: true,
        multiselect: false
    }).navGrid('#pager', { edit: true, add: true, del: true, search: false, refresh: true },
        {
            // edit options
            zIndex: 100,
            url: '/Home/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // add options
            zIndex: 100,
            url: "/Home/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // delete options
            zIndex: 100,
            url: "/Home/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete this task?",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        });

});