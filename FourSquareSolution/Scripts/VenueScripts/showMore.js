$("#show-more").click(function (e) {
    e.preventDefault();
    var searchString = $("#search-input").val();
    var newlyAddedVenuesCount = 10;
    var totalVenuesCount = $("#total-venues-count").val();
    var currentVenuesCount = $("#venues-result-table tr").length;

    var data = {
        searchQuery: searchString,
        currentVenuesCount: currentVenuesCount
    };

    $.ajax({
        url: "/Home/ShowMoreVenues",
        data: data,
        type: "POST",
        success: function (outputHtml) {
            $("#last-row").before(outputHtml);
        }
    });

    if ((totalVenuesCount - (currentVenuesCount + newlyAddedVenuesCount)) < 0) {
        $("#show-more").fadeOut();
    }

});