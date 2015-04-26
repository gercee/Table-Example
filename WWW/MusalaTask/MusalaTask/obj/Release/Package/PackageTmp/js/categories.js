/**
*
* This categories class is used in both pages categories.html and index.html.
* In categories.html is initializeing the complete page.
* In index.html is used only to initialize the drop down for filtering and (adding/updating) documents.
*
* @param String url - is the url that is used to the categories api.
* @param Object editedCategory - is used when editing Category is holding the Category Object for editing.
*
* @method init - initializing the categories.html page
* @method getCategories - send ajax GET request to the server to receive all the categories fom the Categories api and initialize the documents if exists.
* @method updateCategory - send ajax PUT request to update Category object.
* @method addCategory - send ajax POST request to insert new Category object.
* @method deleteCategory - send ajax DELETE request to delete Category object.
* @method initTable - initialize the table of the categories in categories.html.
* @method dateCreatedFormatter - return formated dateCreated in the categories table.
* @method dateModifiedFormatter- return formated dateModified in the categories table.
* @method operateEvents - register click listeners for the buttons in the Action column.
* @method operateFormatter - - return formated html code for the Action column.
*
**/

var categories = {
    url: 'api/categories/',
    editedCategory: null,
    init: function(){
        this.initTable();
        $("#btnAdd").on('click', function (e) {
            $("#btnAdd").prop('disabled', true);
            var name = $("#categoryName").val();
            if (name) {
                if (categories.editedCategory) {
                    categories.editedCategory.name = name;
                    categories.updateCategory(categories.editedCategory);
                } else {
                    var categoryItem = {
                        name: name
                    }
                    categories.addCategory(categoryItem);
                }
            } else {
                $("#btnAdd").prop('disabled', false);
                alert('please insert the category name');
            }
        });

        $('#addCategoryModal').on('hidden.bs.modal', function () {
            $("#btnAdd").prop('disabled', false);
            $("#myModalLabel").html('Add Category');
            categories.editedCategory = null;
            $("#btnAdd").html('Add Category');
            $("#categoryName").val("");
        });

       
 
    },
    
    getCategories: function () {
        $.ajax({
            type: "GET",
            url: categories.url,
            success: function (response) {
                if (documents) {
                    documents.initCategories(response);
                }
            },
            error: function (e) {
                categories.showError(e);
            },
            contentType: "application/json"
        });
    },
    updateCategory: function (category) {
        $.ajax({
            type: "PUT",
            url: categories.url,
            data: JSON.stringify(category),
            success: function (e) {
                $("#btnAdd").prop('disabled', false);
                $("#categoryName").val('');
                $("#addCategoryModal").modal('hide');
                $('#categoriesTable').bootstrapTable('refresh');
            },
            error: function (e) {
                $("#btnAdd").prop('disabled', false);
                categories.showError(e);
            },
            contentType: "application/json"
        });
    },
    showError: function (e) {
        if (e.responseJSON) {
            alert(e.responseJSON.ExceptionMessage);
            return;
        }

        if (e.statusText) {
            alert(e.statusText);
            return;
        }
    },
    addCategory: function (category) {
        $.ajax({
            type: "POST",
            url: categories.url,
            data: JSON.stringify(category),
            success: function (e) {
                $("#btnAdd").prop('disabled', false);
                $("#categoryName").val('');
                $("#addCategoryModal").modal('hide');
                $('#categoriesTable').bootstrapTable('refresh');
            },
            error: function (e) {
                $("#btnAdd").prop('disabled', false);
                categories.showError(e);
            },
            contentType: "application/json"
        });
    },
    deleteCategory: function (category) {
        $.ajax({
            type: "DELETE",
            url: categories.url + category.categoryID,
            data: JSON.stringify(category),
            success: function (e) {
                $('#categoriesTable').bootstrapTable('refresh');
            },
            error: function (e) {
                categories.showError(e);
            },
            contentType: "application/json"
        });
    },

    initTable: function () {
        $('#categoriesTable').bootstrapTable({
            method: 'get',
            url: 'api/categories',
            cache: false,
            striped: true,
            pagination: true,
            pageSize: 50,
            pageList: [10, 25, 50, 100, 200],
            search: true,
            showColumns: true,
            showRefresh: true,
            minimumCountColumns: 2,
            clickToSelect: true,
            columns: [
            {
                field: 'categoryID',
                title: 'Category ID',
                align: 'right',
                valign: 'middle',
                sortable: true
            }, {
                field: 'name',
                title: 'Category Name',
                align: 'center',
                valign: 'middle',
                sortable: true
            }, {
                field: 'dateCreated',
                title: 'Created On',
                align: 'center',
                valign: 'middle',
                formatter: categories.dateCreatedFormatter,
                sortable: true
            }, {
                field: 'dateModified',
                title: 'Modified On',
                align: 'center',
                valign: 'middle',
                formatter: categories.dateModifiedFormatter,
                sortable: true
            },{
                field: 'operate',
                title: 'Action',
                switchable: false,
                formatter: categories.operateFormatter,
                events: categories.operateEvents
            }]
        });
    },
    dateCreatedFormatter: function (value, row, index) {
        return new Date(row.dateCreated).format('dd.mm.yyyy');
    },
    dateModifiedFormatter: function (value, row, index) {
        return new Date(row.dateModified).format('dd.mm.yyyy');
    },
    operateEvents: {
        'click .edit': function (e, value, row, index) {
            categories.editedCategory = row;
            $("#myModalLabel").html('Update Category');
            $("#addCategoryModal").modal('show');
            $("#categoryName").val(row.name);
            $("#btnAdd").html('Update Category');
        },
        'click .remove': function (e, value, row, index) {
            categories.deleteCategory(row);
        }
    },
    operateFormatter: function (value, row, index) {
        return [
            '<a class="btn btn-default edit ml10" href="javascript:void(0)" title="Edit">',
                '<i class="glyphicon glyphicon-edit"></i>',
            '</a>',
            '<a class="btn btn-default remove ml10" href="javascript:void(0)" style="margin-left:10px;" title="Remove">',
                '<i class="glyphicon glyphicon-remove"></i>',
            '</a>'
        ].join('');
    }

}

