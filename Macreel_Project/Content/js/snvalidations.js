function numbersonly(e) {
    var unicode = e.charCode ? e.charCode : e.keyCode
    if (unicode != 8 & unicode != 9) { //if the key isn't the backspace key (which we should allow)
        if (unicode < 48 || unicode > 57 || unicode == 9) //if not a number
        {
            return false //disable key press
        }
    }
}
function Decimalnumbers(e) {
    var unicode = e.charCode ? e.charCode : e.keyCode
    if (unicode != 8 & unicode != 9) { //if the key isn't the backspace key (which we should allow)
        if ((unicode < 48 || unicode > 57 || unicode == 9) && unicode != 46) //if not a number
        {
            return false //disable key press
        }
    }
}
function Blank(e) {
    var unicode = e.charCode ? e.charCode : e.keyCode
    if ((unicode >= 00 && unicode <= 255)) //if not a number
    {
        return false
    }
}
function AlphaSymbols(e) {
    var unicode = e.charCode ? e.charCode : e.keyCode
    if (unicode != 8 & unicode != 9 & unicode != 32) { //if the key isn't the backspace key (which we should allow)
        if ((unicode < 58 && unicode > 47) || unicode > 126 || unicode == 9) //if not a number
        {
            return false
        }
    }
}
function Alphaonly(e) {
    var unicode = e.charCode ? e.charCode : e.keyCode
    if (unicode != 8 & unicode != 9 & unicode != 32) { //if the key isn't the backspace key (which we should allow)

        if (unicode < 65 || (unicode > 90 && unicode < 97) || unicode > 122 || unicode == 9) //if not a number
        {
            return false
        }
    }
}
function Alphanumeric(e) {
    var unicode = e.charCode ? e.charCode : e.keyCode
    if (unicode != 8 & unicode != 9 & unicode != 32) { //if the key isn't the backspace key (which we should allow)
        if ((unicode > 47 && unicode < 58) || (unicode > 64 && unicode < 91) || (unicode > 96 && unicode < 123) || unicode == 9) {

        }
        else {
            return false
        }
    }
}

function regno(e) {
    var unicode = e.charCode ? e.charCode : e.keyCode
    if (unicode != 8 & unicode != 9 & unicode != 32) { //if the key isn't the backspace key (which we should allow)
        if ((unicode > 47 && unicode < 58) || (unicode > 64 && unicode < 91) || (unicode > 96 && unicode < 123) || unicode == 45 || unicode == 47 || unicode == 35 || unicode == 95 || unicode == 9) {

        }
        else {
            return false
        }
    }
}
function AlphSymbolsNotQuotes(e) {
    var unicode = e.charCode ? e.charCode : e.keyCode
    if (unicode != 8 & unicode != 9 & unicode != 32) { //if the key isn't the backspace key (which we should allow)
        if ((unicode < 58 && unicode > 47) || unicode > 126 || unicode == 9 || unicode == 34 || unicode == 39) //if not a number
        {
            return false
        }
    }
}
function AllowMaxLength(e, field, len) {
    var val = field.value;
    if (val.length > len) {
        field.value = val.substr(0, len);
    }
}