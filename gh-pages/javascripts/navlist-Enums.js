//console.log('This would be the main JS file.');

var el = document.getElementById("nav_content");
var navlist = '';
navlist = navlist + '<h1><a href="stdlibXtf.html"><img src="images/icon_namespace.png" alt="n" />stdlibXtf</a></h1>';
navlist = navlist + '<h2><a href="stdlibXtf.Common.html"><img src="images/icon_namespace.png" alt="n" />stdlibXtf.Common</a></h2>';
navlist = navlist + '<h2><a href="stdlibXtf.Enums.html"><img src="images/icon_namespace.png" alt="n" />stdlibXtf.Enums</a></h2>';
navlist = navlist + '<h3><a href="stdlibXtf.Enums.EnumExtensions.html"><img src="images/icon_class.png" alt="c" />EnumExtensions</a></h3>';
navlist = navlist + '<h3><a href="stdlibXtf.Enums.ChannelSampleFormats.html"><img src="images/icon_enum.png" alt="e" />ChannelSampleFormats</a></h3>';
navlist = navlist + '<h3><a href="stdlibXtf.Enums.ChannelTypes.html"><img src="images/icon_enum.png" alt="e" />ChannelTypes</a></h3>';
navlist = navlist + '<h3><a href="stdlibXtf.Enums.CoordinateUnits.html"><img src="images/icon_enum.png" alt="e" />CoordinateUnits</a></h3>';
navlist = navlist + '<h3><a href="stdlibXtf.Enums.CorrectionFlags.html"><img src="images/icon_enum.png" alt="e" />CorrectionFlags</a></h3>';
navlist = navlist + '<h3><a href="stdlibXtf.Enums.DataPolarity.html"><img src="images/icon_enum.png" alt="e" />DataPolarity</a></h3>';
navlist = navlist + '<h3><a href="stdlibXtf.Enums.NoteSubChannels.html"><img src="images/icon_enum.png" alt="e" />NoteSubChannels</a></h3>';
navlist = navlist + '<h2><a href="stdlibXtf.Packets.html"><img src="images/icon_namespace.png" alt="n" />stdlibXtf.Packets</a></h2>';
navlist = navlist + '<h2><a href="stdlibXtf.SubPackets.html"><img src="images/icon_namespace.png" alt="n" />stdlibXtf.SubPackets</a></h2>';
el.innerHTML = navlist;