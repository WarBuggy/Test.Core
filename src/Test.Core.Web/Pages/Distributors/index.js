$(function () {
    var l = abp.localization.getResource("Core");
	var distributorService = window.test.core.controllers.distributors.distributor;
	
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Distributors/CreateModal",
        scriptUrl: "/Pages/Distributors/createModal.js",
        modalClass: "distributorCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Distributors/EditModal",
        scriptUrl: "/Pages/Distributors/editModal.js",
        modalClass: "distributorEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            companyName: $("#CompanyNameFilter").val(),
			taxID: $("#TaxIDFilter").val()
        };
    };

    var dataTable = $("#DistributorsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(distributorService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('Core.Distributors.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('Core.Distributors.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    distributorService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "companyName" },
			{ data: "taxID" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewDistributorButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reload();
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reload();
    });
    
    
});
