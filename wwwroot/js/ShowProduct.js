$(document).ready(function () {


    function paging(options) {

        $.ajax({
            url: `/ProductView/_ProductList`,
            method: "GET",
            data: {
                currentPage: options.currentPage,
                min: options.min,
                max: options.max,
            },
            success: function (data) {
                $("#ProductList").html(data);
            },
            error: function () {
                console.error("Failed to load the partial view.");
            }
        });
    }

    $('th').click(function () {
        var selectedOption = $(this).find(":selected");
        var min = selectedOption.data('min');
        var max = selectedOption.data('max');

        var columnName = $(this).text().trim();
        console.log(columnName);
        debugger;
        $.ajax({
            method: "GET",
            url: "/ProductView/_ProductList",
            data: {
                min: min,
                max: max,
                ColumnName: columnName
            },
            success: function (data) {
                $("#ProductList").html(data); 
            }
        });
    });

    $(".page-link").click(function (e) {
        e.preventDefault();
        var currentPage = $(this).attr("page");
        var min = 0;
        var max = 0;
        var options = {
            currentPage: currentPage,
            min: min,
            max: max
        };
        paging(options);
    });

    $("#searchBox").click(function (e) {
        e.preventDefault();
        var search = $("input[name='Search']").val();

    });

    $("#rangeSelect").on('change', function () {
        var selectedOption = $(this).find(":selected");
        var min = selectedOption.data('min');
        var max = selectedOption.data('max');
        var page = $("#currentPage").val();

        $.ajax({
            url: "/ProductView/FilterProducts",
            method: 'GET',
            data: {
                min: min,
                max: max,
            },
            success: function (response) {
                $("#ProductList").html(response);
                var currentPage = $("#currentPage").val();
                var pageCount = $("#pageCount").val();
                var paginationHtml = '';
                for (var i = 1; i <= pageCount; i++) {
                    paginationHtml += `<a href="#" page="${i}" class="page-link badge rounded-pill bg-light ${i == currentPage ? 'active' : ''}">${i}</a>`;
                }
                $("#pagination").html(paginationHtml);
              
                var options = {
                    min: min,
                    max: max,
                    currentPage: currentPage
                }
                $(".page-link").click(function (e) {
                    e.preventDefault();
                    var newPage = $(this).attr("page");
                    options.currentPage = newPage;                   
                    paging(options);
                });
            }
        });
    });

});