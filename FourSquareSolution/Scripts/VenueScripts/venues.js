$("#show-more").click(function(e) {
    e.preventDefault();
    var searchString = $("#search-input").val();
    var searchQueryCount = $("#venues-result-table tr").length;

    var data = {
        SearchQuery: searchString
    };

    $.ajax({
        url: "/Home/ShowMoreVenues",
        data: data,
        type: "POST",
        success: function(outputHtml) {
            console.log(outputHtml);
            $("#venues-result-table tbody tr > :last").after(outputHtml);
        }
    });

});

$("#search-button").click(function(e) {
    e.preventDefault();
    var searchString = $("#search-input").val();

    var data = {
        SearchQuery: searchString
    };

    $.ajax({
        url: "/Home/SearchVenues",
        data: data,
        type: "POST",
        success: function(outputHtml) {
            $("#venues-result").html(outputHtml);
        }
    });
});

$("#search-clear").click(function() {
    $("#search-input").val("");

    var data = {
        SearchQuery: ""
    };

    $.ajax({
        url: "/Home/SearchVenues",
        data: data,
        type: "POST",
        success: function(outputHtml) {
            $("#venues-result").html(outputHtml);
        }
    });
});