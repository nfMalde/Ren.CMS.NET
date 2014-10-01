	var JavaScriptExecuter = (!window.JavaScriptExecuter ? {} : window.JavaScriptExecuter);
	((function(){
	if(window.JavaScriptExecuter == {}) {
	JavaScriptExecuter.addJS = function(scriptId, func) {
			if (scriptId && func && typeof func == 'function'
					&& typeof scriptId == 'string'
					&& scriptId.trim() == scriptId)
				window.JavaScriptExecuter[scriptId] = func;
			else if (console.error)
				console
						.error('Error In Add Script! Check $addJS calls for the following rules [scriptID, STRING, NOT NULL, NOT NUMERIC, NO SPACES] [func TYPEOF FUNCTION NOT NULL]')
			else
				alert('Error In Add Script! Check $addJS calls for the following rules [scriptID, STRING, NOT NULL, NOT NUMERIC, NO SPACES] [func TYPEOF FUNCTION NOT NULL]')
		};

  JavaScriptExecuter.init = function() {
			
			if (window.$ != undefined || window.jQuery != undefined) {
				if($ == undefined && jQuery != undefined)
					window.$ = jQuery;
				
				$(document).ready(function() {
					var executer = function(script) {
						console.log(script.length);
						 
							for ( var key in script) {
								if(key == undefined)
									continue;
								if (console.log)
									console.log("Executing Script " + key);
								if (typeof script[key] == 'function') {
									var func = script[key];
									func();
								}
							}
					};

					(function(s) {
						executer(s);
					})(JavaScriptExecuter);
					if(console.info)
						console.info('Jquery loaded and jquery scripts executed...');

				});
			}
			else{
				if(console.info)
					console.info('Jquery not initialized. Waiting 100ms');
					
			 	window.setTimeout(JavaScriptExecuter.init, '100');	
			}
		}
	
		};
		}))();
