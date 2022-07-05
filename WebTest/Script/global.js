
$(document).ready(() => {
	var InitIndex = function () {
		$('#main_header').html(main_header_template.innerHTML);
		$('#main_body').html(main_body_template.innerHTML);
		$('#main_footer').html(main_footer_template.innerHTML);
	}
	//初始化
	InitIndex();
	//載入HTML
	window.LoadHtml = function (_url) {
		var CallBack = () => { };
		if (_url == 'Index.html')
			InitIndex();
		else
			$('#main_body').load(_url, null, CallBack);
	}
});