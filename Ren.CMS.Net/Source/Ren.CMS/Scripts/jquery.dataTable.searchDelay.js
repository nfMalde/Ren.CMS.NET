$(function() {
	/**! Search Delay plugin, prototype: Malte Peters
	 * This plugin delays the dataTable search. 
	 * 
	 * **/
	var checkForDt = function() {
		// Store for the <!table!> object
		this.dt = '';
		// Tell the triggers if we hitted enter. If yes execute search directly
		this.hittedEnter = false;
		// Determine if >keyDown trigger is running (only 1 trigger at time) ==
		// (1 Ajax Request per search)
		this.triggerRunning = false;
		// Determine the trigger for checking is user stopped typing is running
		this.notTypingTriggerRunning = false;
		// Stores the information if the user is typing or not
		this.typing = false;
		// New Scope for functions
		var that = this;
		// Checks if a DataTable Exists
		var check = function() {
			// Search whole body for dataTables
			that.dt = $($('body').find("table.dataTable"));
			if (dt.length > 0) {
				// We Found a dataTable. We can go
				return true;
			} else
				return false;
		};

		// Check for DataTable if not found, try again in 10 MS
		if (!check())
			window.setTimeout(checkForDt, '10');
		else {

			// DataTable was found we should reinit the new scope
			var that = this;
			// Trigger if fnDraw() from DataTable was done. If yes we should
			// remove the disabled attr from the search input
			this.dt.on('draw.dt', function() {
				// Getting the Filter fields by the default DataTable Schema:
				// {tableID}_filter
				var filterFields = that.dt.attr("id") + "_filter";
				// Remove attribute "disabled" from search input
				$('#' + filterFields + " input").removeAttr("disabled");
			});

			$(".dataTables_filter input").unbind() // Unbind previous default
													// bindings
			// Add key Up Event to check if user stopped typing
			.keyup(function() {
				// For better checking store the old search value
				that.oldSearchValue = $(this).val();
				// Scope for searchinput
				var sinput = this;
				// Anonymous function to check if user is typing
				var functionIsTyping = function() {
					// Simple compare the value of the search input after 2
					// seconds with the old one to check if user is typing
					if ($(sinput).val() == that.oldSearchValue)
						that.typing = false;
					else
						that.typing = true;
					// Tell the events our trigger is stopping now
					that.notTypingTriggerRunning = false;

				};

				// If trigger is not running we start it now
				if (!that.notTypingTriggerRunning) {
					// Only one trigger per time so set notTypingTriggerRunning
					// to true
					that.notTypingTriggerRunning = true;
					// Start trigger with a delay of 2 Seconds
					window.setTimeout(functionIsTyping, '2000');
				}
			})
			// Key Down Event for executing the search
			.keydown(
					function(event) {
						// Did the user hit enter? if no we are still typing
						if (event.keyCode == '13')
							that.hittedenter = true;
						else
							that.typing = true;

						// Activate search for a minvalue of 3 characters and
						// for empty fields to reset the table, also if we hit
						// enter
						if ($(this).val().length > 3
								|| $(this).val().trim() == ''
								|| that.hittedenter) {
							var input = this;
							var searchTrigger = function() {
								if (that.typing && !that.hittedenter) {
									// Client is typing, checka gain in 2,2
									// Seconds
									window.setTimeout(searchTrigger, '2200');
								} else {

									// Execute search, but first disable the
									// input field
									$(input).attr("disabled", "disabled")
									// Setup new search filter
									that.dt.dataTable()
											.fnFilter($(input).val());
									// Redraw the table
									that.dt.dataTable().fnDraw();
								}
								// Tell us the trigger is stopping
								that.triggerRunning = false;
							};

							// If we hit enter, execute search directly
							if (that.hittedenter && !that.typing) {
								that.dt.dataTable().fnFilter($(input).val());
								that.dt.dataTable().fnDraw();
							} else {

								if (!that.triggerRunning) {
									that.triggerRunning = true;
									window.setTimeout(searchTrigger, '2200');
								}
							}

						}

					});
		}
	};

	// Scrip Execution
	var init = function() {
		if (!$.fn.dataTable) {
			window.setTimeout(init, '10');
		} else {
			checkForDt();
		}
	};
	init();
});
