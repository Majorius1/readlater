$(".category-create").on("click", function (event) {
	event.preventDefault();

	$(".create-category-form").show();
	$(this).hide();
});

function hideForm() {
	$(".create-category-form").hide();
	$(".category-create").show();
}