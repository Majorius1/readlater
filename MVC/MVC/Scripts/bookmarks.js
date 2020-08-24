$(".category-create").on("click", function (event) {
	event.preventDefault();

	$(".create-category-form").show();
	$(this).hide();
});

$(".bookmark-link > a").on("click", function (event) {
	event.preventDefault();

	$(this).closest("form")[0].submit();
});

function hideForm() {
	$(".create-category-form").hide();
	$(".category-create").show();
}