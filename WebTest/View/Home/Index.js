
function Submit() {
	var Input = $('#Name').val();
	$.ajax({
		url: '../../Handler/HomeHandler.ashx/DumpData',
		method: 'post',
		dataType: 'json',
		data: {
			InputText: Input
		},
		success: function (res) {
			alert(res);
		},
		error: function (err) { console.log(err); alert('錯誤'); },
	});
}
