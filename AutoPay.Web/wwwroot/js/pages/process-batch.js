var approvedCustomers = [];

async function caputureCharges() {
    $("#modal-payment-progress").modal({
        backdrop: "static",
        keyboard: false
    });

    if (approvedCustomers.length === 0) {
        alertify.error("Please select a customer before processing.");
        return;
    }

    const $progressBar = $("#modal-payment-progress .progress-bar");
    const $chargeStatus = $("#modal-payment-progress #charge-status");
    const totalCount = approvedCustomers.length;
    const progressStep = 100 / totalCount;

    $progressBar.css("width", "0%");
    $chargeStatus.html(`0/${totalCount} Completed.`);

    try {
        for (let i = 0; i < approvedCustomers.length; i++) {

            await new Promise((resolve, reject) => {
                var id = approvedCustomers[i];
                $.ajax({
                    url: `/batch/charge/${id}`,
                    type: 'POST',
                    success: function () {
                        resolve();
                    },
                    error: function (a, b) {
                        a.then((c) => {
                            console.log(c);
                        });

                        reject();
                    }
                });
            });

            $progressBar.css("width", (progressStep * (i + 1)) + "%");
            $chargeStatus.html(`${i + 1}/${totalCount} Completed!`);
        }

        updateBatchStatus();

    } catch (e) {
        $("#modal-payment-progress").modal("hide");
        alertify.error("Something went wrong. Please check the log for the detailed error.");
    }
}

function setChargeButton() {
    if (approvedCustomers.length === 0) {
        $("#btnCaptureCharges").prop("disabled", "disabled");
    } else {
        $("#btnCaptureCharges").removeAttr("disabled");
    }
}

function updateBatchStatus() {
    const batchId = $("#Id").val();
    $.post(`/batch/updateStatus/${batchId}`,
        function () {
            location.href = "/batch/manage";
        }).fail(function (res) {
            alertify.error("Charges has been captured but failed to update.");
            location.href = "/batch/manage";
        });
}

function showEditAmountModal(id, amountDue) {
    $("#hidCustomerId").val(id);
    $("#AmountDue").val(amountDue);
    appUtils.showLoader(".box-body");
    $.get(`/batch/getAmountDueDetail/${id}`,
        function (res) {
            updateAmountDueDetailModal(res);
            appUtils.hideLoader(".box-body");
            $("#modal-update-amount").modal({
                backdrop: "static",
                keyboard: false
            });
        }).fail(function (res) {
            appUtils.hideLoader(".box-body");
            appUtils.processErrorResponse(res);
        });

}

function updateAmountDueDetailModal(items) {
    var htmlString = "";
    $.each(items,
        function (index, item) {
            if (item.recType !== 2) {
                console.log(item);
                const label = item.recType === 0 ? "Starting Balance" : item.description;
                htmlString +=
                    `<div class="form-group">
                        <label class="control-label col-md-5"> ${label}:</label>
                        <div class="col-md-7"> 
                            <div class="input-group">
                                <span class="input-group-addon">
                                    <i class="fa fa-dollar"></i>
                                </span>
                                <input type="text" class="form-control disabled" disabled value="${item.amountDue}"/>
                            </div>              
                        </div>
                    </div>`;
            }
        });
    $("#container-amount-due-history").html(htmlString);
}

function editAmount() {

    const model = {
        id: $("#hidCustomerId").val(),
        amountDue: $("#AmountDue").val()
    };

    if (appUtils.isNullOrEmpty(model.amountDue)) {
        alertify.error("Please enter an amount.");
        return;
    }

    if (parseFloat(model.amountDue) <= 0) {
        alertify.error("Please enter a valid amount.");
        return;
    }

    appUtils.showLoader("#modal-update-amount .modal-content");
    $.post("/batch/updateAmountDue", model,
        function () {
            location.reload();
        }).fail(function (res) {
            appUtils.hideLoader("#modal-update-amount .modal-content");
            appUtils.processErrorResponse(res);
        });
}

function closeEditAmountModal() {
    $("#hidCustomerId").val(null);
    $("#AmountDue").val(null);
    $("#container-amount-due-history").html("");
    $("#modal-update-amount").modal("hide");
}

$(document).ready(function () {
    $("#table-current-charges tbody tr #action-approve input[type='checkbox']").each(
        function () {
            $(this).on("ifChecked", function (event) {
                const id = event.target.id.replace("approve_", "");
                approvedCustomers.push(id);
                setChargeButton();
            });
            $(this).on("ifUnchecked", function (event) {
                const id = event.target.id.replace("approve_", "");
                approvedCustomers.splice($.inArray(id, approvedCustomers), 1);
                setChargeButton();
            });
        });
});