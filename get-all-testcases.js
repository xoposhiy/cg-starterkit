// Run it in browser console to get all tescases printed to console

for(let tc of angular.element("div.question-infos").scope().apis.testcases.testcases){
    $.ajax({
            url: "https://www.codingame.com/servlet/fileservlet?id=" + tc.inputBinaryId,
            dataType: "text",
            async: true,
            success: function(data) {
                console.log(tc.index +' "' + data + '",');
            },
            error: function(dobj, data, err) {
                alert("Error : " + data + " : " + err);
            }
        });
}