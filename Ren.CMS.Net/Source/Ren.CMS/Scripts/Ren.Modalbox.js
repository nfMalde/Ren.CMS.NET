(function ($) {

	$.fn.RenModalBox = function () {
		$(this).click(function(){
			
			if ($(this).attr("id") == null || $(this).attr("id") == "") {

				console.error("RenModalBox Needs unique id for Trigger Element!");


			}
			else {

				var div = '<div  id="'+ $(this).attr("id") +'_MODAL_BOX" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">' +
					  '<div class="modal-dialog modal-lg">' +
					 '<div class="modal-content">' +
					  '</div>' +
					  '</div>' +
					 '</div>';

				if ($(this).get(0).tagName.toLowerCase() == "a") {

					var options = {
						remote: $(this).attr("href"),
						show:true
					};

					$("body").append(div);
					$('#' + $(this).attr("id") + '_MODAL_BOX').modal(options);
					$('#' + $(this).attr("id") + '_MODAL_BOX').on('hidden.bs.modal', function (e) {

						$(this).remove();

					});

				}
			}
				

		});
		};






	





}(Jquery));