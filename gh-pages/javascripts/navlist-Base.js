//console.log('This would be the main JS file.');

var el = document.getElementById("nav_content");
var navlist = '';
navlist = navlist + '<h1><a href="stdlibXtf.html"><img src="images/icon_namespace.png" alt="n" />stdlibXtf</a></h1>';
navlist = navlist + '<h2><a href="stdlibXtf.ArrayToStream.html"><img src="images/icon_class.png" alt="c" />ArrayToStream</a></h2>';
navlist = navlist + '<h2><a href="stdlibXtf.PacketSniffer.html"><img src="images/icon_class.png" alt="c" />PacketSniffer</a></h2>';
navlist = navlist + '<h2><a href="stdlibXtf.XtfDocument.html"><img src="images/icon_class.png" alt="c" />XtfDocument</a></h2>';
navlist = navlist + '<h2><a href="stdlibXtf.XtfMainHeader.html"><img src="images/icon_class.png" alt="c" />XtfMainHeader</a></h2>';
navlist = navlist + '<h2><a href="stdlibXtf.Common.html"><img src="images/icon_namespace.png" alt="n" />stdlibXtf.Common</a></h2>';
navlist = navlist + '<h2><a href="stdlibXtf.Enums.html"><img src="images/icon_namespace.png" alt="n" />stdlibXtf.Enums</a></h2>';
navlist = navlist + '<h2><a href="stdlibXtf.Packets.html"><img src="images/icon_namespace.png" alt="n" />stdlibXtf.Packets</a></h2>';
navlist = navlist + '<h2><a href="stdlibXtf.SubPackets.html"><img src="images/icon_namespace.png" alt="n" />stdlibXtf.SubPackets</a></h2>';
el.innerHTML = navlist;