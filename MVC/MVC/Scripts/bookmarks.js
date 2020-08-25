$(function () {
	$(".category-create").on("click", function (event) {
		event.preventDefault();

		$(".create-category-form").show();
		$(this).hide();
	});

	$(".bookmark-link > a").on("click", function (event) {
		event.preventDefault();

		$(this).closest("form")[0].submit();
	});

	$('[data-toggle="tooltip"]').tooltip();
	$(".glyphicon-copy").on("click", function () {
		const textInput = this.firstElementChild;
		copyToClipboad(textInput);
		$(this).attr("title", "Copied!").tooltip("fixTitle").tooltip("show");
	});
});

function hideForm() {
	$(".create-category-form").hide();
	$(".category-create").show();
}

function copyToClipboad(textInput) {
	textInput.type = "text";
	textInput.select();
	textInput.setSelectionRange(0, 99999);
	document.execCommand("copy");
	textInput.type = "hidden";
}
