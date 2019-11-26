$('[data-open-modal]').click(async function () {
    event.preventDefault();
    $('#movieModalBody').hide();
    $('#movieModalPreloader').show();
    $('#movieModal').modal('show');

    let url = $(this).attr('href');
    let response = await fetch(url);
    let result = await response.text();
    $('#movieModalBody').html(result);

    $('#movieModalPreloader').hide();
    $('#movieModalBody').show();
});

let page;
let totalPages;
let url;

function initPagination(p, t, u) {
    page = p;
    totalPages = t;
    url = u;
}

$(window).scroll(async function () {
    if ($(window).scrollTop() + $(window).height() > $(document).height() - 100) {
        page++;

        if (page > totalPages)
            return;

        let response = await fetch(`${url}&page=${page}`);
        let html = await response.text();
        $('#movieResults').append(html);
    }
});

$('#buttonNext').click(async function () {
    page++;
    let response = await fetch(`${url}&page=${page}`);
    let html = await response.text();
    $('#movieResults').append(html);

    if (page === totalPages) {
        $(this).remove();
    }
});