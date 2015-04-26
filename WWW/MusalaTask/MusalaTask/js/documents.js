/**
*
* This documents class is used in both pages index.html.
* In index.html is initializeing the complete page.
*
* @param String url - is the url that is used to the documents api.
* @param String urlUpload - is the url that is used to the upload api.
*
* @method init - initializing the index.html page
* @method downloadURI - used for downloading document from server.
* @method updateDocument - send ajax PUT request to update Document object.
* @method addDocument - send ajax POST request to insert new Document object.
* @method initCategories - used to initialize the categories drop down components.
* @method deleteDocument - send ajax DELETE request to delete Document object.
* @method initTable - initialize the table of the documents in index.html.
* @method dateCreatedFormatter - return formated dateCreated in the documents table.
* @method dateModifiedFormatter- return formated dateModified in the documents table.
* @method operateEvents - register click listeners for the buttons in the Action column.
* @method operateFormatter - - return formated html code for the Action column.
*
**/

var documents = {
    url: 'api/documents/',
    urlUpload: 'api/upload/',
    init: function () {
        $("#btnSave").on('click', function () {
            $("#btnSave").prop('disabled', true);
            var name = $("#documentName").val();
            var description = $("#documentDescription").val();
            var categoryID = $("#documentCategory").val();
            var files = $("#fileupload").get(0).files;

            if (!name) {
                alert('Please write the document name.');
                $("#btnSave").prop('disabled', false);
                return;
            }

            if (!categoryID) {
                alert('Please select category.');
                $("#btnSave").prop('disabled', false);
                return;
            }



            if (documents.editedDocument) {

                documents.editedDocument.name = name;
                documents.editedDocument.description = description;
                documents.editedDocument.categoryID = categoryID;
                documents.updateDocument(documents.editedDocument);
            }
            else {
                if (!files.length) {
                    alert('Please select file.');
                    $("#btnSave").prop('disabled', false);
                    return;
                }

                if (files.length > 0) {
                    var data = new FormData();
                    for (i = 0; i < files.length; i++) {
                        data.append("file" + i, files[i]);
                    }
                    $.ajax({
                        type: "POST",
                        url: documents.urlUpload,
                        contentType: false,
                        processData: false,
                        data: data,
                        success: function (response) {
                            if (response.isSuccessful) {
                                var docItem = {
                                    name: name,
                                    description: description,
                                    categoryID: categoryID,
                                    location: response.message
                                };
                                documents.addDocument(docItem);
                            }
                        },
                        error: function () {
                            $("#btnSave").prop('disabled', false);
                            alert("Error while invoking the Web API");
                        }
                    });
                }
            }

        });

        this.initTable();

        $('#addDocumentModal').on('hidden.bs.modal', function () {
            $("#btnSave").prop('disabled', false);
            $("#myModalLabel").html('Add Document');
            documents.editedDocument = null;
            $("#btnSave").html('Add Document');
            $("#documentName").val("");
            $("#documentDescription").val("");
            $("#uploadFile").show();
        });

        $("#documentCategoryFilter").on('change', function () {
            var categoryIDCur = $(this).val();

            if (categoryIDCur == -1) {
                $('#documentsTable').bootstrapTable('filterBy', {
                    
                });
            } else {

                var rex = new RegExp($(this).val(), 'i');
                $('#documentsTable tbody tr').hide();
                $('#documentsTable tbody tr').filter(function () {
                    var rowVal = $(this).context.cells[0].innerHTML;
                    return parseInt(rowVal) == parseInt(categoryIDCur);
                }).show();

            }
            
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
    downloadURI: function (uri, name) {
        console.log(uri);
        var link = document.createElement("a");
        link.download = name;
        link.href = uri;
        link.click();
    },
    deleteDocument: function (document) {
        $.ajax({
            type: "DELETE",
            url: documents.url + document.documentID,
            success: function (e) {
                $('#documentsTable').bootstrapTable('refresh');
                $('#documentCategoryFilter').val(-1);
            },
            error: function (e) {
                documents.showError(e);
            },
            contentType: "application/json"
        });

    },
    updateDocument: function (document) {
        console.log(document);
        $.ajax({
            type: "PUT",
            url: documents.url,
            data: JSON.stringify(document),
            success: function (e) {
                $("#addDocumentModal").modal('hide');
                $('#documentsTable').bootstrapTable('refresh');
                $('#documentCategoryFilter').val(-1);
                $("#btnSave").prop('disabled', false);
            },
            error: function (e) {
                $("#btnSave").prop('disabled', false);
                documents.showError(e);
            },
            contentType: "application/json"
        });

    },
    addDocument: function (document) {
        console.log(document);
        $.ajax({
            type: "POST",
            url: documents.url,
            data: JSON.stringify(document),
            success: function (e) {
                $("#btnSave").prop('disabled', false);
                $("#addDocumentModal").modal('hide');
                $('#documentsTable').bootstrapTable('refresh');
                $('#documentCategoryFilter').val(-1);
            },
            error: function (e) {
                documents.showError(e);
            },
            contentType: "application/json"
        });

    },
    initCategories: function (response) {
        var categoryStr = '';
        for (var i = 0; i < response.length; i++) {
            var category = response[i];
            categoryStr += '<option value="' + category.categoryID + '">' + category.name + '</option>';
        }
        $("#documentCategory").html(categoryStr);
        var filter = '<option value="-1"></option>' + categoryStr;
        $("#documentCategoryFilter").html(filter);
    },

    initTable: function () {
        $('#documentsTable').bootstrapTable({
            method: 'get',
            url: 'api/Documents',
            cache: false,
            striped: true,
            pagination: true,
            pageSize: 50,
            pageList: [10, 25, 50, 100, 200],
            search: true,
            showColumns: true,
            showFilter: true,
            showRefresh: true,
            minimumCountColumns: 2,
            clickToSelect: true,
            columns: [

            {
                field: 'categoryID',
                title: 'Category ID',
                align: 'center',
                valign: 'middle',
                sortable: true,
                switchable: false,
                class: 'hiddenColumn'
            },
            {
                field: 'documentID',
                title: 'Document ID',
                align: 'right',
                valign: 'middle',
                sortable: true
            }, {
                field: 'name',
                title: 'Document Name',
                align: 'center',
                valign: 'middle',
                sortable: true
            },
            {
                field: 'categoryName',
                title: 'Category',
                align: 'center',
                valign: 'middle',
                sortable: true
            },
            {
                field: 'description',
                title: 'Description',
                align: 'left',
                valign: 'middle'
            }, {
                field: 'dateCreated',
                title: 'Created On',
                align: 'center',
                valign: 'middle',
                formatter: documents.dateCreatedFormatter,
                sortable: true
            }, {
                field: 'dateModified',
                title: 'Modified On',
                align: 'center',
                valign: 'middle',
                formatter: documents.dateModifiedFormatter,
                sortable: true
            }, {
                field: 'operate',
                title: 'Action',
                switchable: false,
                formatter: documents.operateFormatter,
                events: documents.operateEvents,
                class: 'action'
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
            documents.editedDocument = row;
            $("#addDocumentModal").modal('show');
            $("#myModalLabel").html('Update Document');
            $("#documentName").val(row.name);
            $("#documentDescription").val(row.description);
            $("#documentCategory").val(row.categoryID);
            $("#uploadFile").hide();
            $("#btnSave").html('Update Document');
        },
        'click .remove': function (e, value, row, index) {
            documents.deleteDocument(row);
        },
        'click .download': function (e, value, row, index) {
            documents.downloadURI(row.location, row.name);
        }
    },
    operateFormatter: function (value, row, index) {
        return [
            '<a class="btn btn-default edit ml10" href="javascript:void(0)" title="Edit">',
                '<i class="glyphicon glyphicon-edit"></i>',
            '</a>',
             '<a class="btn btn-default download ml10" href="javascript:void(0)" style="margin-left:10px;" title="Download">',
                '<i class="glyphicon glyphicon-download-alt"></i>',
            '</a>',
             '<a class="btn btn-default remove ml10" href="javascript:void(0)" style="margin-left:10px;" title="Remove">',
                '<i class="glyphicon glyphicon-remove"></i>',
            '</a>'
        ].join('');
    }
}





