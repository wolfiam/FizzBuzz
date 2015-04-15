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
    }).navGrid('#pager', { edit: false, add: false, del: false, search: false, refresh: false })
});
