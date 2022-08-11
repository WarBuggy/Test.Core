$(function () {
    var l = abp.localization.getResource("TestMDM");

    var distributorService = window.testMDM.distributors.distributor;


    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "TestMDM/Distributors/CreateModal",
        scriptUrl: "/Pages/TestMDM/Distributors/createModal.js",
        modalClass: "distributorCreate"
    });

    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "TestMDM/Distributors/EditModal",
        scriptUrl: "/Pages/TestMDM/Distributors/editModal.js",
        modalClass: "distributorEdit"
    });

    var getFilter = function () {
        return {
            filterText: $("#FilterText").val(),
            companyName: $("#CompanyNameFilter").val(),
            taxId: $("#TaxIdFilter").val(),
            identityUserId: $("#IdentityUserFilter").val()
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
                                visible: abp.auth.isGranted('TestMDM.Distributors.Edit'),
                                action: function (data) {
                                    editModal.open({
                                        id: data.record.distributor.id
                                    });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('TestMDM.Distributors.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    distributorService.delete(data.record.distributor.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
            { data: "distributor.companyName" },
            { data: "distributor.taxId" },
            {
                data: "identityUsers",
                render: function (data, type, row) {
                    if (!data || !Array.isArray(data)) {
                        return "";
                    }
                    let strings = [];
                    for (let i = 0; i < data.length; i++) {
                        let user = data[i];
                        strings.push(`<li>${user.name}, ${user.email}</li>`);
                    }
                    return strings.join("");
                }
            }
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

    $('#AdvancedFilterSection select').change(function () {
        dataTable.ajax.reload();
    });

    $('#IdentityUserFilter').select2({
        ajax: {
            url: abp.appPath + 'api/test-m-d-m/distributors/identity-user-lookup',
            type: 'GET',
            data: function (params) {
                return { filter: params.term, maxResultCount: 10 }
            },
            processResults: function (data) {
                var mappedItems = _.map(data.items, function (item) {
                    return { id: item.id, text: item.displayName };
                });
                mappedItems.unshift({ id: "", text: ' - ' });

                return { results: mappedItems };
            }
        }
    });

});
