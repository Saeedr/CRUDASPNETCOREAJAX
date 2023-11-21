// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    $("#loaderbody").addClass('hide');
    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
});

showInPopup = (urlX, title) => {
    $.ajax({
        type: "GET",
        url: urlX,
        success: function (ress) {
            $('#form-modal .modal-body').html(ress);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    })
}

jQueryAjaxtPost = form => {
    try {
        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $("#View-all").html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                    $.notify('submitted successfully', { globalPosition:'top center',className:'success' })
                }
                else {
                    $("#form-modal .modal-body").html(res.html)

                }

            },
            error: function (err) {
                console.log(err);
            }
        })
    }
    catch (e) {
        console.log(e);
    }
    return false;
}

jQueryAjaxDelete = form => {
    if (confirm("Are you sure to delete this record?")) {
        try {
            $.ajax({
                type: "POST",
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $("#View-all").html(res.html)
                    $.notify('Delete successfully', { globalPosition: 'top center', className: 'Delete' })


                },
                error: function (err) {
                    console.log(err);
                }
            })

        } catch (e) {
            console.log(e);
        }
    }
    return false;
}


