window.modalHelper = {
    show: function (id) {
        var modal = new bootstrap.Modal(document.getElementById(id));
        modal.show();
    },
    hide: function (id) {
        var modal = bootstrap.Modal.getInstance(document.getElementById(id));
        if (modal) modal.hide();
    }
};