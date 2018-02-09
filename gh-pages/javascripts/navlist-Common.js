//console.log('This would be the main JS file.');

var el = document.getElementById("nav_content");
var navlist = '';
navlist = navlist + '<h1><a href="stdlibXtf.html"><img src="images/icon_namespace.png" alt="n" />stdlibXtf</a></h1>';
navlist = navlist + '<h2><a href="stdlibXtf.Common.html"><img src="images/icon_namespace.png" alt="n" />stdlibXtf.Common</a></h2>';
navlist = navlist + '<h3><a href="stdlibXtf.Common.IndexEntry.html"><img src="images/icon_class.png" alt="c" />IndexEntry</a></h3>';
navlist = navlist + '<h3><a href="stdlibXtf.Common.PacketHeaderTypes.html"><img src="images/icon_class.png" alt="c" />PacketHeaderTypes</a></h3>';
navlist = navlist + '<h3><a href="stdlibXtf.Common.SonarModelTypes.html"><img src="images/icon_class.png" alt="c" />SonarModelTypes</a></h3>';
navlist = navlist + '<h3><a href="stdlibXtf.Common.StatCollection.html"><img src="images/icon_class.png" alt="c" />StatCollection</a></h3>';
navlist = navlist + '<h3><a href="stdlibXtf.Common.StatEntry.html"><img src="images/icon_class.png" alt="c" />StatEntry</a></h3>';
navlist = navlist + '<h3><a href="stdlibXtf.Common.TypeEntry.html"><img src="images/icon_class.png" alt="c" />TypeEntry</a></h3>';
navlist = navlist + '<h3><a href="stdlibXtf.Common.IExports.html"><img src="images/icon_interface.png" alt="i" />IExports</a></h3>';
navlist = navlist + '<h3><a href="stdlibXtf.Common.IPacket.html"><img src="images/icon_interface.png" alt="i" />IPacket</a></h3>';
navlist = navlist + '<h2><a href="stdlibXtf.Enums.html"><img src="images/icon_namespace.png" alt="n" />stdlibXtf.Enums</a></h2>';
navlist = navlist + '<h2><a href="stdlibXtf.Packets.html"><img src="images/icon_namespace.png" alt="n" />stdlibXtf.Packets</a></h2>';
navlist = navlist + '<h2><a href="stdlibXtf.SubPackets.html"><img src="images/icon_namespace.png" alt="n" />stdlibXtf.SubPackets</a></h2>';
el.innerHTML = navlist;