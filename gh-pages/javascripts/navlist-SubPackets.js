//console.log('This would be the main JS file.');

var el = document.getElementById("nav_content");
var navlist = '';
navlist = navlist + '<h1><a href="stdlibXtf.html"><img src="images/icon_namespace.png" alt="n" />stdlibXtf</a></h1>';
navlist = navlist + '<h2><a href="stdlibXtf.Common.html"><img src="images/icon_namespace.png" alt="n" />stdlibXtf.Common</a></h2>';
navlist = navlist + '<h2><a href="stdlibXtf.Enums.html"><img src="images/icon_namespace.png" alt="n" />stdlibXtf.Enums</a></h2>';
navlist = navlist + '<h2><a href="stdlibXtf.Packets.html"><img src="images/icon_namespace.png" alt="n" />stdlibXtf.Packets</a></h2>';
navlist = navlist + '<h2><a href="stdlibXtf.SubPackets.html"><img src="images/icon_namespace.png" alt="n" />stdlibXtf.SubPackets</a></h2>';
navlist = navlist + '<h3><a href="stdlibXtf.SubPackets.BathySnippet0.html"><img src="images/icon_class.png" alt="c" />BathySnippet0</a></h3>';
navlist = navlist + '<h3><a href="stdlibXtf.SubPackets.BathySnippet1.html"><img src="images/icon_class.png" alt="c" />BathySnippet1</a></h3>';
navlist = navlist + '<h3><a href="stdlibXtf.SubPackets.BeamXYZA.html"><img src="images/icon_class.png" alt="c" />BeamXYZA</a></h3>';
navlist = navlist + '<h3><a href="stdlibXtf.SubPackets.ChannelInfo.html"><img src="images/icon_class.png" alt="c" />ChannelInfo</a></h3>';
navlist = navlist + '<h3><a href="stdlibXtf.SubPackets.PingChannelHeader.html"><img src="images/icon_class.png" alt="c" />PingChannelHeader</a></h3>';
navlist = navlist + '<h3><a href="stdlibXtf.SubPackets.QpsMbeEntry.html"><img src="images/icon_class.png" alt="c" />QpsMbeEntry</a></h3>';
navlist = navlist + '<h3><a href="stdlibXtf.SubPackets.QpsMultiTxEntry.html"><img src="images/icon_class.png" alt="c" />QpsMultiTxEntry</a></h3>';
el.innerHTML = navlist;