namespace Ren.CMS.CORE.FileManagement
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    using NHibernate;

    using Ren.CMS.CORE.nhibernate;
    using Ren.CMS.CORE.nhibernate.Base;
    using Ren.CMS.CORE.nhibernate.Domain;
    using Ren.CMS.Persistence.Domain;
    using Ren.CMS.Persistence.Repositories;

    public class FileManagement
    {
        #region Fields

        private static IDictionary<string, string> _mappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {

        #region Big freaking list of mime types
        // combination of values from Windows 7 Registry and
        // from C:\Windows\System32\inetsrv\config\applicationHost.config
        // some added, including .7z and .dat
        {".323", "text/h323"},
        {".3g2", "video/3gpp2"},
        {".3gp", "video/3gpp"},
        {".3gp2", "video/3gpp2"},
        {".3gpp", "video/3gpp"},
        {".7z", "application/x-7z-compressed"},
        {".aa", "audio/audible"},
        {".AAC", "audio/aac"},
        {".aaf", "application/octet-stream"},
        {".aax", "audio/vnd.audible.aax"},
        {".ac3", "audio/ac3"},
        {".aca", "application/octet-stream"},
        {".accda", "application/msaccess.addin"},
        {".accdb", "application/msaccess"},
        {".accdc", "application/msaccess.cab"},
        {".accde", "application/msaccess"},
        {".accdr", "application/msaccess.runtime"},
        {".accdt", "application/msaccess"},
        {".accdw", "application/msaccess.webapplication"},
        {".accft", "application/msaccess.ftemplate"},
        {".acx", "application/internet-property-stream"},
        {".AddIn", "text/xml"},
        {".ade", "application/msaccess"},
        {".adobebridge", "application/x-bridge-url"},
        {".adp", "application/msaccess"},
        {".ADT", "audio/vnd.dlna.adts"},
        {".ADTS", "audio/aac"},
        {".afm", "application/octet-stream"},
        {".ai", "application/postscript"},
        {".aif", "audio/x-aiff"},
        {".aifc", "audio/aiff"},
        {".aiff", "audio/aiff"},
        {".air", "application/vnd.adobe.air-application-installer-package+zip"},
        {".amc", "application/x-mpeg"},
        {".application", "application/x-ms-application"},
        {".art", "image/x-jg"},
        {".asa", "application/xml"},
        {".asax", "application/xml"},
        {".ascx", "application/xml"},
        {".asd", "application/octet-stream"},
        {".asf", "video/x-ms-asf"},
        {".ashx", "application/xml"},
        {".asi", "application/octet-stream"},
        {".asm", "text/plain"},
        {".asmx", "application/xml"},
        {".aspx", "application/xml"},
        {".asr", "video/x-ms-asf"},
        {".asx", "video/x-ms-asf"},
        {".atom", "application/atom+xml"},
        {".au", "audio/basic"},
        {".avi", "video/x-msvideo"},
        {".axs", "application/olescript"},
        {".bas", "text/plain"},
        {".bcpio", "application/x-bcpio"},
        {".bin", "application/octet-stream"},
        {".bmp", "image/bmp"},
        {".c", "text/plain"},
        {".cab", "application/octet-stream"},
        {".caf", "audio/x-caf"},
        {".calx", "application/vnd.ms-office.calx"},
        {".cat", "application/vnd.ms-pki.seccat"},
        {".cc", "text/plain"},
        {".cd", "text/plain"},
        {".cdda", "audio/aiff"},
        {".cdf", "application/x-cdf"},
        {".cer", "application/x-x509-ca-cert"},
        {".chm", "application/octet-stream"},
        {".class", "application/x-java-applet"},
        {".clp", "application/x-msclip"},
        {".cmx", "image/x-cmx"},
        {".cnf", "text/plain"},
        {".cod", "image/cis-cod"},
        {".config", "application/xml"},
        {".contact", "text/x-ms-contact"},
        {".coverage", "application/xml"},
        {".cpio", "application/x-cpio"},
        {".cpp", "text/plain"},
        {".crd", "application/x-mscardfile"},
        {".crl", "application/pkix-crl"},
        {".crt", "application/x-x509-ca-cert"},
        {".cs", "text/plain"},
        {".csdproj", "text/plain"},
        {".csh", "application/x-csh"},
        {".csproj", "text/plain"},
        {".css", "text/css"},
        {".csv", "text/csv"},
        {".cur", "application/octet-stream"},
        {".cxx", "text/plain"},
        {".dat", "application/octet-stream"},
        {".datasource", "application/xml"},
        {".dbproj", "text/plain"},
        {".dcr", "application/x-director"},
        {".def", "text/plain"},
        {".deploy", "application/octet-stream"},
        {".der", "application/x-x509-ca-cert"},
        {".dgml", "application/xml"},
        {".dib", "image/bmp"},
        {".dif", "video/x-dv"},
        {".dir", "application/x-director"},
        {".disco", "text/xml"},
        {".dll", "application/x-msdownload"},
        {".dll.config", "text/xml"},
        {".dlm", "text/dlm"},
        {".doc", "application/msword"},
        {".docm", "application/vnd.ms-word.document.macroEnabled.12"},
        {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
        {".dot", "application/msword"},
        {".dotm", "application/vnd.ms-word.template.macroEnabled.12"},
        {".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
        {".dsp", "application/octet-stream"},
        {".dsw", "text/plain"},
        {".dtd", "text/xml"},
        {".dtsConfig", "text/xml"},
        {".dv", "video/x-dv"},
        {".dvi", "application/x-dvi"},
        {".dwf", "drawing/x-dwf"},
        {".dwp", "application/octet-stream"},
        {".dxr", "application/x-director"},
        {".eml", "message/rfc822"},
        {".emz", "application/octet-stream"},
        {".eot", "application/octet-stream"},
        {".eps", "application/postscript"},
        {".etl", "application/etl"},
        {".etx", "text/x-setext"},
        {".evy", "application/envoy"},
        {".exe", "application/octet-stream"},
        {".exe.config", "text/xml"},
        {".fdf", "application/vnd.fdf"},
        {".fif", "application/fractals"},
        {".filters", "Application/xml"},
        {".fla", "application/octet-stream"},
        {".flr", "x-world/x-vrml"},
        {".flv", "video/x-flv"},
        {".fsscript", "application/fsharp-script"},
        {".fsx", "application/fsharp-script"},
        {".generictest", "application/xml"},
        {".gif", "image/gif"},
        {".group", "text/x-ms-group"},
        {".gsm", "audio/x-gsm"},
        {".gtar", "application/x-gtar"},
        {".gz", "application/x-gzip"},
        {".h", "text/plain"},
        {".hdf", "application/x-hdf"},
        {".hdml", "text/x-hdml"},
        {".hhc", "application/x-oleobject"},
        {".hhk", "application/octet-stream"},
        {".hhp", "application/octet-stream"},
        {".hlp", "application/winhlp"},
        {".hpp", "text/plain"},
        {".hqx", "application/mac-binhex40"},
        {".hta", "application/hta"},
        {".htc", "text/x-component"},
        {".htm", "text/html"},
        {".html", "text/html"},
        {".htt", "text/webviewhtml"},
        {".hxa", "application/xml"},
        {".hxc", "application/xml"},
        {".hxd", "application/octet-stream"},
        {".hxe", "application/xml"},
        {".hxf", "application/xml"},
        {".hxh", "application/octet-stream"},
        {".hxi", "application/octet-stream"},
        {".hxk", "application/xml"},
        {".hxq", "application/octet-stream"},
        {".hxr", "application/octet-stream"},
        {".hxs", "application/octet-stream"},
        {".hxt", "text/html"},
        {".hxv", "application/xml"},
        {".hxw", "application/octet-stream"},
        {".hxx", "text/plain"},
        {".i", "text/plain"},
        {".ico", "image/x-icon"},
        {".ics", "application/octet-stream"},
        {".idl", "text/plain"},
        {".ief", "image/ief"},
        {".iii", "application/x-iphone"},
        {".inc", "text/plain"},
        {".inf", "application/octet-stream"},
        {".inl", "text/plain"},
        {".ins", "application/x-internet-signup"},
        {".ipa", "application/x-itunes-ipa"},
        {".ipg", "application/x-itunes-ipg"},
        {".ipproj", "text/plain"},
        {".ipsw", "application/x-itunes-ipsw"},
        {".iqy", "text/x-ms-iqy"},
        {".isp", "application/x-internet-signup"},
        {".ite", "application/x-itunes-ite"},
        {".itlp", "application/x-itunes-itlp"},
        {".itms", "application/x-itunes-itms"},
        {".itpc", "application/x-itunes-itpc"},
        {".IVF", "video/x-ivf"},
        {".jar", "application/java-archive"},
        {".java", "application/octet-stream"},
        {".jck", "application/liquidmotion"},
        {".jcz", "application/liquidmotion"},
        {".jfif", "image/pjpeg"},
        {".jnlp", "application/x-java-jnlp-file"},
        {".jpb", "application/octet-stream"},
        {".jpe", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".jpg", "image/jpeg"},
        {".js", "application/x-javascript"},
        {".jsx", "text/jscript"},
        {".jsxbin", "text/plain"},
        {".latex", "application/x-latex"},
        {".library-ms", "application/windows-library+xml"},
        {".lit", "application/x-ms-reader"},
        {".loadtest", "application/xml"},
        {".lpk", "application/octet-stream"},
        {".lsf", "video/x-la-asf"},
        {".lst", "text/plain"},
        {".lsx", "video/x-la-asf"},
        {".lzh", "application/octet-stream"},
        {".m13", "application/x-msmediaview"},
        {".m14", "application/x-msmediaview"},
        {".m1v", "video/mpeg"},
        {".m2t", "video/vnd.dlna.mpeg-tts"},
        {".m2ts", "video/vnd.dlna.mpeg-tts"},
        {".m2v", "video/mpeg"},
        {".m3u", "audio/x-mpegurl"},
        {".m3u8", "audio/x-mpegurl"},
        {".m4a", "audio/m4a"},
        {".m4b", "audio/m4b"},
        {".m4p", "audio/m4p"},
        {".m4r", "audio/x-m4r"},
        {".m4v", "video/x-m4v"},
        {".mac", "image/x-macpaint"},
        {".mak", "text/plain"},
        {".man", "application/x-troff-man"},
        {".manifest", "application/x-ms-manifest"},
        {".map", "text/plain"},
        {".master", "application/xml"},
        {".mda", "application/msaccess"},
        {".mdb", "application/x-msaccess"},
        {".mde", "application/msaccess"},
        {".mdp", "application/octet-stream"},
        {".me", "application/x-troff-me"},
        {".mfp", "application/x-shockwave-flash"},
        {".mht", "message/rfc822"},
        {".mhtml", "message/rfc822"},
        {".mid", "audio/mid"},
        {".midi", "audio/mid"},
        {".mix", "application/octet-stream"},
        {".mk", "text/plain"},
        {".mmf", "application/x-smaf"},
        {".mno", "text/xml"},
        {".mny", "application/x-msmoney"},
        {".mod", "video/mpeg"},
        {".mov", "video/quicktime"},
        {".movie", "video/x-sgi-movie"},
        {".mp2", "video/mpeg"},
        {".mp2v", "video/mpeg"},
        {".mp3", "audio/mpeg"},
        {".mp4", "video/mp4"},
        {".mp4v", "video/mp4"},
        {".mpa", "video/mpeg"},
        {".mpe", "video/mpeg"},
        {".mpeg", "video/mpeg"},
        {".mpf", "application/vnd.ms-mediapackage"},
        {".mpg", "video/mpeg"},
        {".mpp", "application/vnd.ms-project"},
        {".mpv2", "video/mpeg"},
        {".mqv", "video/quicktime"},
        {".ms", "application/x-troff-ms"},
        {".msi", "application/octet-stream"},
        {".mso", "application/octet-stream"},
        {".mts", "video/vnd.dlna.mpeg-tts"},
        {".mtx", "application/xml"},
        {".mvb", "application/x-msmediaview"},
        {".mvc", "application/x-miva-compiled"},
        {".mxp", "application/x-mmxp"},
        {".nc", "application/x-netcdf"},
        {".nsc", "video/x-ms-asf"},
        {".nws", "message/rfc822"},
        {".ocx", "application/octet-stream"},
        {".oda", "application/oda"},
        {".odc", "text/x-ms-odc"},
        {".odh", "text/plain"},
        {".odl", "text/plain"},
        {".odp", "application/vnd.oasis.opendocument.presentation"},
        {".ods", "application/oleobject"},
        {".odt", "application/vnd.oasis.opendocument.text"},
        {".one", "application/onenote"},
        {".onea", "application/onenote"},
        {".onepkg", "application/onenote"},
        {".onetmp", "application/onenote"},
        {".onetoc", "application/onenote"},
        {".onetoc2", "application/onenote"},
        {".orderedtest", "application/xml"},
        {".osdx", "application/opensearchdescription+xml"},
        {".p10", "application/pkcs10"},
        {".p12", "application/x-pkcs12"},
        {".p7b", "application/x-pkcs7-certificates"},
        {".p7c", "application/pkcs7-mime"},
        {".p7m", "application/pkcs7-mime"},
        {".p7r", "application/x-pkcs7-certreqresp"},
        {".p7s", "application/pkcs7-signature"},
        {".pbm", "image/x-portable-bitmap"},
        {".pcast", "application/x-podcast"},
        {".pct", "image/pict"},
        {".pcx", "application/octet-stream"},
        {".pcz", "application/octet-stream"},
        {".pdf", "application/pdf"},
        {".pfb", "application/octet-stream"},
        {".pfm", "application/octet-stream"},
        {".pfx", "application/x-pkcs12"},
        {".pgm", "image/x-portable-graymap"},
        {".pic", "image/pict"},
        {".pict", "image/pict"},
        {".pkgdef", "text/plain"},
        {".pkgundef", "text/plain"},
        {".pko", "application/vnd.ms-pki.pko"},
        {".pls", "audio/scpls"},
        {".pma", "application/x-perfmon"},
        {".pmc", "application/x-perfmon"},
        {".pml", "application/x-perfmon"},
        {".pmr", "application/x-perfmon"},
        {".pmw", "application/x-perfmon"},
        {".png", "image/png"},
        {".pnm", "image/x-portable-anymap"},
        {".pnt", "image/x-macpaint"},
        {".pntg", "image/x-macpaint"},
        {".pnz", "image/png"},
        {".pot", "application/vnd.ms-powerpoint"},
        {".potm", "application/vnd.ms-powerpoint.template.macroEnabled.12"},
        {".potx", "application/vnd.openxmlformats-officedocument.presentationml.template"},
        {".ppa", "application/vnd.ms-powerpoint"},
        {".ppam", "application/vnd.ms-powerpoint.addin.macroEnabled.12"},
        {".ppm", "image/x-portable-pixmap"},
        {".pps", "application/vnd.ms-powerpoint"},
        {".ppsm", "application/vnd.ms-powerpoint.slideshow.macroEnabled.12"},
        {".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
        {".ppt", "application/vnd.ms-powerpoint"},
        {".pptm", "application/vnd.ms-powerpoint.presentation.macroEnabled.12"},
        {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
        {".prf", "application/pics-rules"},
        {".prm", "application/octet-stream"},
        {".prx", "application/octet-stream"},
        {".ps", "application/postscript"},
        {".psc1", "application/PowerShell"},
        {".psd", "application/octet-stream"},
        {".psess", "application/xml"},
        {".psm", "application/octet-stream"},
        {".psp", "application/octet-stream"},
        {".pub", "application/x-mspublisher"},
        {".pwz", "application/vnd.ms-powerpoint"},
        {".qht", "text/x-html-insertion"},
        {".qhtm", "text/x-html-insertion"},
        {".qt", "video/quicktime"},
        {".qti", "image/x-quicktime"},
        {".qtif", "image/x-quicktime"},
        {".qtl", "application/x-quicktimeplayer"},
        {".qxd", "application/octet-stream"},
        {".ra", "audio/x-pn-realaudio"},
        {".ram", "audio/x-pn-realaudio"},
        {".rar", "application/octet-stream"},
        {".ras", "image/x-cmu-raster"},
        {".rat", "application/rat-file"},
        {".rc", "text/plain"},
        {".rc2", "text/plain"},
        {".rct", "text/plain"},
        {".rdlc", "application/xml"},
        {".resx", "application/xml"},
        {".rf", "image/vnd.rn-realflash"},
        {".rgb", "image/x-rgb"},
        {".rgs", "text/plain"},
        {".rm", "application/vnd.rn-realmedia"},
        {".rmi", "audio/mid"},
        {".rmp", "application/vnd.rn-rn_music_package"},
        {".roff", "application/x-troff"},
        {".rpm", "audio/x-pn-realaudio-plugin"},
        {".rqy", "text/x-ms-rqy"},
        {".rtf", "application/rtf"},
        {".rtx", "text/richtext"},
        {".ruleset", "application/xml"},
        {".s", "text/plain"},
        {".safariextz", "application/x-safari-safariextz"},
        {".scd", "application/x-msschedule"},
        {".sct", "text/scriptlet"},
        {".sd2", "audio/x-sd2"},
        {".sdp", "application/sdp"},
        {".sea", "application/octet-stream"},
        {".searchConnector-ms", "application/windows-search-connector+xml"},
        {".setpay", "application/set-payment-initiation"},
        {".setreg", "application/set-registration-initiation"},
        {".settings", "application/xml"},
        {".sgimb", "application/x-sgimb"},
        {".sgml", "text/sgml"},
        {".sh", "application/x-sh"},
        {".shar", "application/x-shar"},
        {".shtml", "text/html"},
        {".sit", "application/x-stuffit"},
        {".sitemap", "application/xml"},
        {".skin", "application/xml"},
        {".sldm", "application/vnd.ms-powerpoint.slide.macroEnabled.12"},
        {".sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide"},
        {".slk", "application/vnd.ms-excel"},
        {".sln", "text/plain"},
        {".slupkg-ms", "application/x-ms-license"},
        {".smd", "audio/x-smd"},
        {".smi", "application/octet-stream"},
        {".smx", "audio/x-smd"},
        {".smz", "audio/x-smd"},
        {".snd", "audio/basic"},
        {".snippet", "application/xml"},
        {".snp", "application/octet-stream"},
        {".sol", "text/plain"},
        {".sor", "text/plain"},
        {".spc", "application/x-pkcs7-certificates"},
        {".spl", "application/futuresplash"},
        {".src", "application/x-wais-source"},
        {".srf", "text/plain"},
        {".SSISDeploymentManifest", "text/xml"},
        {".ssm", "application/streamingmedia"},
        {".sst", "application/vnd.ms-pki.certstore"},
        {".stl", "application/vnd.ms-pki.stl"},
        {".sv4cpio", "application/x-sv4cpio"},
        {".sv4crc", "application/x-sv4crc"},
        {".svc", "application/xml"},
        {".swf", "application/x-shockwave-flash"},
        {".t", "application/x-troff"},
        {".tar", "application/x-tar"},
        {".tcl", "application/x-tcl"},
        {".testrunconfig", "application/xml"},
        {".testsettings", "application/xml"},
        {".tex", "application/x-tex"},
        {".texi", "application/x-texinfo"},
        {".texinfo", "application/x-texinfo"},
        {".tgz", "application/x-compressed"},
        {".thmx", "application/vnd.ms-officetheme"},
        {".thn", "application/octet-stream"},
        {".tif", "image/tiff"},
        {".tiff", "image/tiff"},
        {".tlh", "text/plain"},
        {".tli", "text/plain"},
        {".toc", "application/octet-stream"},
        {".tr", "application/x-troff"},
        {".trm", "application/x-msterminal"},
        {".trx", "application/xml"},
        {".ts", "video/vnd.dlna.mpeg-tts"},
        {".tsv", "text/tab-separated-values"},
        {".ttf", "application/octet-stream"},
        {".tts", "video/vnd.dlna.mpeg-tts"},
        {".txt", "text/plain"},
        {".u32", "application/octet-stream"},
        {".uls", "text/iuls"},
        {".user", "text/plain"},
        {".ustar", "application/x-ustar"},
        {".vb", "text/plain"},
        {".vbdproj", "text/plain"},
        {".vbk", "video/mpeg"},
        {".vbproj", "text/plain"},
        {".vbs", "text/vbscript"},
        {".vcf", "text/x-vcard"},
        {".vcproj", "Application/xml"},
        {".vcs", "text/plain"},
        {".vcxproj", "Application/xml"},
        {".vddproj", "text/plain"},
        {".vdp", "text/plain"},
        {".vdproj", "text/plain"},
        {".vdx", "application/vnd.ms-visio.viewer"},
        {".vml", "text/xml"},
        {".vscontent", "application/xml"},
        {".vsct", "text/xml"},
        {".vsd", "application/vnd.visio"},
        {".vsi", "application/ms-vsi"},
        {".vsix", "application/vsix"},
        {".vsixlangpack", "text/xml"},
        {".vsixmanifest", "text/xml"},
        {".vsmdi", "application/xml"},
        {".vspscc", "text/plain"},
        {".vss", "application/vnd.visio"},
        {".vsscc", "text/plain"},
        {".vssettings", "text/xml"},
        {".vssscc", "text/plain"},
        {".vst", "application/vnd.visio"},
        {".vstemplate", "text/xml"},
        {".vsto", "application/x-ms-vsto"},
        {".vsw", "application/vnd.visio"},
        {".vsx", "application/vnd.visio"},
        {".vtx", "application/vnd.visio"},
        {".wav", "audio/wav"},
        {".wave", "audio/wav"},
        {".wax", "audio/x-ms-wax"},
        {".wbk", "application/msword"},
        {".wbmp", "image/vnd.wap.wbmp"},
        {".wcm", "application/vnd.ms-works"},
        {".wdb", "application/vnd.ms-works"},
        {".wdp", "image/vnd.ms-photo"},
        {".webarchive", "application/x-safari-webarchive"},
        {".webtest", "application/xml"},
        {".wiq", "application/xml"},
        {".wiz", "application/msword"},
        {".wks", "application/vnd.ms-works"},
        {".WLMP", "application/wlmoviemaker"},
        {".wlpginstall", "application/x-wlpg-detect"},
        {".wlpginstall3", "application/x-wlpg3-detect"},
        {".wm", "video/x-ms-wm"},
        {".wma", "audio/x-ms-wma"},
        {".wmd", "application/x-ms-wmd"},
        {".wmf", "application/x-msmetafile"},
        {".wml", "text/vnd.wap.wml"},
        {".wmlc", "application/vnd.wap.wmlc"},
        {".wmls", "text/vnd.wap.wmlscript"},
        {".wmlsc", "application/vnd.wap.wmlscriptc"},
        {".wmp", "video/x-ms-wmp"},
        {".wmv", "video/x-ms-wmv"},
        {".wmx", "video/x-ms-wmx"},
        {".wmz", "application/x-ms-wmz"},
        {".wpl", "application/vnd.ms-wpl"},
        {".wps", "application/vnd.ms-works"},
        {".wri", "application/x-mswrite"},
        {".wrl", "x-world/x-vrml"},
        {".wrz", "x-world/x-vrml"},
        {".wsc", "text/scriptlet"},
        {".wsdl", "text/xml"},
        {".wvx", "video/x-ms-wvx"},
        {".x", "application/directx"},
        {".xaf", "x-world/x-vrml"},
        {".xaml", "application/xaml+xml"},
        {".xap", "application/x-silverlight-app"},
        {".xbap", "application/x-ms-xbap"},
        {".xbm", "image/x-xbitmap"},
        {".xdr", "text/plain"},
        {".xht", "application/xhtml+xml"},
        {".xhtml", "application/xhtml+xml"},
        {".xla", "application/vnd.ms-excel"},
        {".xlam", "application/vnd.ms-excel.addin.macroEnabled.12"},
        {".xlc", "application/vnd.ms-excel"},
        {".xld", "application/vnd.ms-excel"},
        {".xlk", "application/vnd.ms-excel"},
        {".xll", "application/vnd.ms-excel"},
        {".xlm", "application/vnd.ms-excel"},
        {".xls", "application/vnd.ms-excel"},
        {".xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12"},
        {".xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12"},
        {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
        {".xlt", "application/vnd.ms-excel"},
        {".xltm", "application/vnd.ms-excel.template.macroEnabled.12"},
        {".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
        {".xlw", "application/vnd.ms-excel"},
        {".xml", "text/xml"},
        {".xmta", "application/xml"},
        {".xof", "x-world/x-vrml"},
        {".XOML", "text/plain"},
        {".xpm", "image/x-xpixmap"},
        {".xps", "application/vnd.ms-xpsdocument"},
        {".xrm-ms", "text/xml"},
        {".xsc", "application/xml"},
        {".xsd", "text/xml"},
        {".xsf", "text/xml"},
        {".xsl", "text/xml"},
        {".xslt", "text/xml"},
        {".xsn", "application/octet-stream"},
        {".xss", "application/xml"},
        {".xtp", "application/octet-stream"},
        {".xwd", "image/x-xwindowdump"},
        {".z", "application/x-compress"},
        {".zip", "application/x-zip-compressed"},
        #endregion

        };

        string[] knownImageExt = {".cod",
        ".ras",
        ".fif",
        ".gif",
        ".ief",
        ".jpeg",
        ".jpg",
        ".jpe",
        ".png",
        ".tiff",
        ".tif",
        ".mcf",
        ".wbmp",
        ".fh4",
        ".fh5",
        ".fhc",
        ".ico",
        ".pnm",
        ".pbm",
        ".pgm",
        ".ppm",
        ".rgb",
        ".xwd",
        ".xbm",
        ".xpm"};
        private string[] knownImageMime = {"image/cis-cod",	
        "image/cmu-raster",
        "image/fif"	,
        "image/gif"	,
        "image/ief"	,
        "image/jpeg"	,
        "image/jpeg",
        "image/jpeg",
        "image/png",
        "image/tiff",
        "image/tiff",
        "image/vasa",
        "image/vnd.wap.wbmp",
        "image/x-freehand",
        "image/x-freehand",
        "image/x-freehand",
        "image/x-icon" ,
        "image/x-portable-anymap",
        "image/x-portable-bitmap",
        "image/x-portable-graymap",
        "image/x-portable-pixmap",
        "image/x-rgb",
        "image/x-windowdump",
        "image/x-xbitmap" ,
        "image/x-xpixmap"  };
        string[] knownVideoExt = {".mpeg",
        ".avi",
        ".movie",
        ".mpg",
        ".mpe",
        ".qt",
        ".mov",
        ".viv",
        ".vivo",
        ".mp4",
        ".m4v",
        ".wmv",
        ".ogg",
        ".ogv",
        ".webm"
                                 };
        string[] knownVideoMime = {"video/mpeg",
        "video/x-msvideo",
        "video/x-sgi-movie",
        "video/mpeg",
        "video/mpeg",
        "video/quicktime",
        "video/quicktime",
        "video/vnd.vivo",
        "video/vnd.vivo",
        "video/mp4",
        "video/mp4",

        "video/x-ms-wmv",
        "video/ogg",
        "video/ogg",
        "video/webm"
                                  };
        private nFile lastFile = null;

        /*
         * Structure dbo.nfcms_Files
         * id int identity(1,1)
         * fpath text
         * aliasName varchar(255)

         * allow2groups varchar(255)
         * active int
         * Needed Globalsetting:
         *
         * FILEMANAGEMENT_WATERMARK
         * FILEMANAGEMENT_REPLACE_IMAGE
         * FILEMANAGEMENT_REPLACE_VIDEO
         *
         * **/
        private int lastFileId = 0;

        #endregion Fields

        #region Methods

        public bool ConvertFile(nFile file, string ffmpegPath, string targetExtension=null)
        {
            FilemanagementCrossBrowsersRepository RepoCrs = new FilemanagementCrossBrowsersRepository();
            List<Ren.CMS.Persistence.Domain.FilemanagementCrossBrowsers> fCol = RepoCrs.GetAll().ToList();

            if (targetExtension != null)
            {
                var Dummy = new Ren.CMS.Persistence.Domain.FilemanagementCrossBrowsers();
                Dummy.FileFormat = (targetExtension.StartsWith(".") ?
                                           targetExtension.Substring(1)
                                           :
                                           targetExtension);

                var isxX = RepoCrs.GetOne(NHibernate.Criterion.Expression.Where<Ren.CMS.Persistence.Domain.FilemanagementCrossBrowsers>(e => e.FileFormat == Dummy.FileFormat));
                if (isxX == null || isxX.Id < 1)
                {
                    fCol.Add(Dummy);
                }
            }
            string old = HttpContext.Current.Server.MapPath(file.filepath);
            foreach (FilemanagementCrossBrowsers format in fCol)
            {

                string proccessParameter = "-y -i \"{0}\" \"{1}\"";
                string processParameterFF = proccessParameter;

                proccessParameter = String.Format(proccessParameter,
                   HttpContext.Current.Server.MapPath(file.filepath),

                    HttpContext.Current.Server.MapPath(file.filepath + "."+ format.FileFormat));

               this.RunProcess(ffmpegPath, proccessParameter);

            }

            file.filepath = file.filepath + targetExtension;
            file.aliasName = Path.GetFileNameWithoutExtension(file.aliasName) + "." + fCol.FirstOrDefault().FileFormat;
            var Edit = EditFile(file);
            if (Edit)
            {
                System.IO.File.Delete(old);

            }
            return true;
        }

        public void DeleteFile(string aliasName)
        {
            nFile F = this.getFile(aliasName, false);

            string fname = Path.GetFileNameWithoutExtension(F.filepath);
            string fFull = Path.GetFileName(F.filepath);
            string path = F.filepath.Replace(fFull, "");
            List<string> files2Delete = new List<string>() { F.filepath };

            if (F.mimetype.ToLower().StartsWith("video"))
            {
                FilemanagementCrossBrowsersRepository CR = new FilemanagementCrossBrowsersRepository();

                var list = CR.GetAll();

                list.ToList().ForEach(e => files2Delete.Add(

                    HttpContext.Current.Server.MapPath(path + fname + "." + e.FileFormat)
                    ));
            }

            foreach (string p in files2Delete)
            {

                if (System.IO.File.Exists(p))
                {

                    System.IO.File.Delete(p);

                }
            }
            SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();

            string q = "DELETE " + new ThisApplication.ThisApplication().getSqlPrefix + "Files WHERE id=@id";
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
            PCOL.Add("@id", F.id);
            SQL.SysConnect();
            SQL.SysNonQuery(q, PCOL);
            SQL.SysDisconnect();
        }

        public bool EditFile(nFile f)
        {
            BaseRepository<Ren.CMS.CORE.nhibernate.Domain.File> repo = new BaseRepository<nhibernate.Domain.File>();
            Ren.CMS.CORE.nhibernate.Domain.File fx = repo.GetOne(
                expression: NHibernate.Criterion.Expression.Where<Ren.CMS.CORE.nhibernate.Domain.File>(fo => fo.Id == f.id));

            if (fx == null) return false;

            fx.Fpath = f.filepath;
            fx.Active = f.isActive == true ? 1 : 0;
            fx.AliasName = f.aliasName;
            fx.FileSize = Convert.ToInt32(new FileInfo(HttpContext.Current.Server.MapPath(f.filepath)).Length);
            fx.NeedPermission = f.needPermission;
            fx.ProfileID = f.ProfileID;

            repo.Update(fx);
            string mime =
                (
                _mappings.Any(e => e.Key.EndsWith( Path.GetExtension(f.filepath) )) ?
                _mappings.Where(e => e.Key.EndsWith(Path.GetExtension(f.filepath))).First().Value
                :
                "stream/download");

            RegisterExtension(
            (Path.GetExtension(f.filepath).StartsWith(".") ?
            Path.GetExtension(f.filepath) :
            "." + Path.GetExtension(f.filepath))
            , mime);

            return true;
        }

        /// <summary>
        /// Gets the replacement file for a given filename. Image files will recieve a 404 Image and Videos a 404 Video
        /// </summary>
        /// <param name="aliasName">aliasName of the image that was not found</param>
        /// <returns>nFile Model</returns>
        public nFile getErrorFile(string aliasName = "error.jpg")
        {
            return this.get404File(aliasName);
        }

        public nFile getFile(string name, bool fileIsActive = true)
        {
            int isActive = 1;
            if (!fileIsActive) isActive = 0;

            BaseRepository<nhibernate.Domain.File> FileRepo = new BaseRepository<nhibernate.Domain.File>();

            var fR = (isActive == 0 ? FileRepo.GetOne(NHibernate.Criterion.Expression.Where<nhibernate.Domain.File>(e => e.AliasName == name)) : FileRepo.GetOne(NHibernate.Criterion.Expression.Where<nhibernate.Domain.File>(e => e.AliasName == name && e.Active == 1)));

            if (fR != null)
            {

                FilemanagementCrossBrowsersRepository fmx =  new FilemanagementCrossBrowsersRepository();

                string browserExt = (fmx.GetByBrowserID(HttpContext.Current.Request.Browser.Browser) ?? fmx.GetDefault() ?? new FilemanagementCrossBrowsers()).FileFormat;

                string pName = Path.GetFileNameWithoutExtension(fR.Fpath) + "." + browserExt;
                string myPath =
                    (fR.Fpath.Replace(Path.GetFileName(fR.Fpath), ""));

                var f = new nFile()
                {
                    aliasName = fR.AliasName,
                    filepath = fR.Fpath,
                    id = fR.Id,
                    isActive = (fR.Active == 1),
                    ProfileID = (Convert.ToInt32(fR.ProfileID)),
                    needPermission = fR.NeedPermission,
                    mimetype = this.getMIMETypeForExtension(Path.GetExtension(fR.Fpath))
                };

                if (f.mimetype.ToLower().StartsWith("video"))
                {
                    string browser = HttpContext.Current.Request.Browser.Browser.ToLower();
                    string newPath = myPath + pName;
                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(newPath)))
                    {
                        f.filepath = newPath;
                        f.mimetype = this.getMIMETypeForExtension(Path.GetExtension(f.filepath));
                    }
                }

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(f.filepath)))
                {

                    return f;
                }
                else
                {

                    return this.get404File(name);

                }

            }
            else
            {

                return this.get404File(name);

            }
        }

        public string getMIMETypeForExtension(string extension)
        {
            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            Sql.SysConnect();
            ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
            string query = "SELECT TOP 1 MIMEType FROM " + TA.getSqlPrefix + "RegisteredMIMETypes WHERE fileExstension=@ext";
            SqlHelper.nSqlParameterCollection Parameters = new SqlHelper.nSqlParameterCollection();
            Parameters.Add("@ext", extension);
            string mime = "application/octet-stream";
            SqlDataReader R = Sql.SysReader(query, Parameters);

            if (R.HasRows)
            {
                R.Read();

                mime = (string)R["MIMEType"];

            }
            if (mime == "" || mime == null || mime == "application/octet-stream")
            {
                for (int i = 0; i < this.knownVideoExt.Length; i++)
                {
                    mime = (this.knownVideoExt[i] == extension ?
                        (this.knownVideoMime[i] != null ? this.knownVideoMime[i] : "") : mime);
                }
            }
            R.Close();

            Sql.SysDisconnect();

            return mime;
        }

        public string getVideoThumpnailRawImage(Guid attachID, string ffmegPath)
        {
            Ren.CMS.CORE.nhibernate.Repositories.ContentAttachmentRepository Repo = new Ren.CMS.CORE.nhibernate.Repositories.ContentAttachmentRepository();

            var attachment = Repo.GetByPKid(attachID);

            if (attachment == null) throw new Exception("Attachment not found!");

            FileManagement FX = new FileManagement();

            var f = FX.getFile(attachment.FName, false);
            string video = f.filepath;
            if (f == null || f.id < 1)
            {

                video = attachment.FPath + "/" + attachment.FName;

            }

            string[] thmppath = {
                                                    "~/Binaries",
                                                    "~/Binaries/Converter",
                                                    "~/Binaries/Converter/Videothumpnails"
                                                    , "~/Binaries/Converter/Videothumpnails/"+ attachment.Pkid
                                                };

            foreach (string d in thmppath)
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(d)))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(d));
            //Run FFMPEG
            string WorkingDirectory = thmppath.Last();
            WorkingDirectory = HttpContext.Current.Server.MapPath(WorkingDirectory);
            if (!System.IO.Directory.GetFiles(WorkingDirectory).Any(e => Path.GetFileName(e).StartsWith("thump") && Path.GetFileName(e).EndsWith(".jpg")))
            {
                string output = WorkingDirectory + "\\thump.%d.jpg";
                string ffmpegParameter = "-i \"{0}\" -f image2 -vf fps=fps=1/10 \"{1}\""; //Every TEN seconds.
                ffmpegParameter = String.Format(ffmpegParameter,
                    HttpContext.Current.Server.MapPath(video),
                    output);

                while (FX.RunProcess(ffmegPath, ffmpegParameter) != "ok") ;
            }
            var FileList = System.IO.Directory.GetFiles(WorkingDirectory);

            int i = FileList.Length - 1;
            //Got max index

            if (i > 2)
            {
                return FileList[i / 2];
            }
            return FileList.Last();
        }

        public bool isImage(HttpPostedFileBase F)
        {
            string CT = F.ContentType.ToLower();

            string EXT = Path.GetExtension(F.FileName).ToLower();

            foreach (string c in this.knownImageMime)
            {

                if (c == CT) return true;

            }

            foreach (string ext in this.knownImageExt)
            {
                if (EXT == ext) return true;

            }

            return false;
        }

        public bool isVideo(HttpPostedFileBase F)
        {
            string CT = F.ContentType;

            string EXT = Path.GetExtension(F.FileName);

            foreach (string c in this.knownVideoMime)
            {

                if (c == CT) return true;

            }

            foreach (string ext in this.knownVideoExt)
            {
                if (EXT == ext) return true;

            }

            return false;
        }

        /// <summary>
        /// Registers an file extension to the filemanagement system. Usually there is no need to execute this. After registering a new file the contenttype and extension will be registered too.
        /// </summary>
        /// <param name="extension">The File Extension with dot.  (Example: .jpg, .jpeg, .gif)</param>
        /// <param name="mimetype">The MIME Type of the File for example  image/Jpeg</param>
        public void RegisterExtension(string extension, string mimetype)
        {
            this._registerExtension(extension, mimetype);
        }

        /// <summary>
        /// Register a single file to the FileManagementSystem.
        /// </summary>
        /// <param name="File">nFile Model for registering</param>
        /// <param name="postFieldName">HTTP Post Field Name</param>
        /// <param name="allowedExtensions">Optional: A String array of allowed extensions (.ext,.txt,.aspx ...etc)</param>
        /// <param name="deleteExistingFile">If true, files with the nFile.aliasName of the uploaded File will be deleted before.</param>
        public void RegisterFile(nFile File, string postFieldName, bool deleteExistingFile = false, string[] allowedExtensions = null)
        {
            if (deleteExistingFile == true) this._unregisterFile(File.aliasName);

            this._registerFile(File, postFieldName, allowedExtensions);
        }

        /// <summary>
        /// Register a single file to the FileManagementSystem.
        /// </summary>
        /// <param name="File">nFile Model for registering</param>
        /// <param name="postFieldName">HTTP Post Field Name</param>
        /// <param name="allowedExtensions">Optional: A String array of allowed extensions (.ext,.txt,.aspx ...etc)</param>
        /// <param name="deleteExistingFile">If true, files with the nFile.aliasName of the uploaded File will be deleted before.</param>
        public void RegisterFile(nFile File, HttpPostedFileBase postFieldName, bool deleteExistingFile = false, string[] allowedExtensions = null)
        {
            if (deleteExistingFile == true) this._unregisterFile(File.aliasName);

            this._registerFile(File, postFieldName, allowedExtensions);
        }

        /// <summary>
        /// Registers a couple of files to the FileManagementSystem. Requires one postfield with type "file" per  File
        /// </summary>
        /// <param name="File">nFile Model Array for registering</param>
        /// <param name="postFieldNames">String Array of postFieldNames</param>
        /// <param name="allowedExtensions">Optional: A String array of allowed extensions (.ext,.txt,.aspx ...etc)</param>
        /// <param name="deleteExistingFiles">If true, files with the nFile.aliasName of the uploaded File will be deleted before.</param>
        public void RegisterFiles(nFile[] File, string[] postFieldNames, bool deleteExistingFiles = false, string[] allowedExtensions = null)
        {
            if (File.Length == postFieldNames.Length)
            {

                for (int x = 0; x < File.Length; x++)
                {

                    if (deleteExistingFiles == true) this._unregisterFile(File[x].aliasName);

                    this._registerFile(File[x], postFieldNames[x], allowedExtensions);

                }

            }
        }

        public string RunProcess(string ffmpegExe, string cmd)
        {
            //Get the application path
            string exepath = ffmpegExe;
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = exepath;
            //Path of exe that will be executed, only for "filebuffer" it will be "wmvtool2.exe"
            proc.StartInfo.Arguments = cmd;
            //The command which will be executed
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.RedirectStandardOutput = false;
            proc.Start();

            while (proc.HasExited == false)
            { }
            return "ok";
        }

        private nFile get404File(string name)
        {
            Settings.GlobalSettings GS = new Settings.GlobalSettings();
            nFile f404 = new nFile();

            f404.aliasName = name;
            f404.mimetype = this.getMIMETypeForExtension(Path.GetExtension(name).ToLower());
            if (f404.mimetype.ToLower().StartsWith("video/"))
            {
                object rpv = GS.getSetting("FILEMANAGEMENT_REPLACE_VIDEO").Value;
                if (rpv == null || rpv == "")
                {
                    rpv = "/Storage/Default/replacement_default.flv";
                    f404.mimetype = this.getMIMETypeForExtension(".flv");
                }
                f404.filepath = rpv.ToString();

            }
            else if (f404.mimetype.ToLower().StartsWith("image/"))
            {

                object rpv = GS.getSetting("FILEMANAGEMENT_REPLACE_IMAGE").Value;
                if (rpv == null || rpv == "")
                {
                    rpv = "/Storage/Default/replacement_default.jpg";
                    f404.mimetype = this.getMIMETypeForExtension(".jpg");
                }
                f404.filepath = rpv.ToString();

            }
            else
            {

                f404.filepath = "/Storage/Default/filenotfound.html";
                f404.mimetype = this.getMIMETypeForExtension(".html");

            }

            return f404;
        }

        private void _registerExtension(string ext, string mimetype)
        {
            ext = ext.ToLower();
            mimetype = mimetype.ToLower();
            for (int x = 0; x < this.knownImageExt.Length; x++)
            {
                if (this.knownImageExt[x] == ext)
                {
                    if (this.knownImageMime.Length > x)
                    {

                        mimetype = this.knownImageMime[x];

                    }

                }

            }

            for (int y = 0; y < this.knownVideoExt.Length; y++)
            {
                if (this.knownVideoExt[y] == ext)
                {
                    if (this.knownVideoMime.Length > y)
                    {

                        mimetype = this.knownVideoMime[y];

                    }

                }

            }
            ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
            string query = "SELECT fileExstension,MIMEType FROM " + TA.getSqlPrefix + "RegisteredMIMETypes WHERE fileExstension=@ext";
            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
            PCOL.Add("@ext", ext);

            SqlHelper.nSqlParameterCollection PCOL2 = new SqlHelper.nSqlParameterCollection();
            PCOL2.Add("@ext", ext);
            PCOL2.Add("@mime", mimetype);

            Sql.SysConnect();

            string registerExt = "INSERT INTO " + TA.getSqlPrefix + "RegisteredMIMETypes (fileExstension, MIMEType) VALUES(@ext,@mime)";
            string updateExt = "UPDATE " + TA.getSqlPrefix + "RegisteredMIMETypes SET MIMEType=@mime WHERE fileExstension=@ext";
            SqlDataReader R = Sql.SysReader(query, PCOL);
            if (R.HasRows)
            {
                R.Read();

                string mime2 = R["MIMEType"].ToString();

                R.Close();
                if (mimetype != mime2)
                {

                    Sql.SysNonQuery(updateExt, PCOL2);

                }

            }
            else
            {
                R.Close();
                Sql.SysNonQuery(registerExt, PCOL2);

            }

            if (!R.IsClosed) R.Close();

            Sql.SysDisconnect();
        }

        private void _registerFile(nFile Filep, string postFieldName, string[] allowedExtensions = null)
        {
            if (!String.IsNullOrEmpty(Filep.filepath))
            {

                SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
                SqlHelper.nSqlParameterCollection SqlPara = new SqlHelper.nSqlParameterCollection();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                //Handle the File

                string path = HttpContext.Current.Request.Files[postFieldName].FileName;
                path = HttpContext.Current.Server.MapPath(path);

                //Generate Filename

                string targetPath = Filep.filepath;
                if (targetPath.Contains("\\"))
                {

                    string rootpath = HttpContext.Current.Server.MapPath("~/");
                    //Get Mappath to the Root

                    targetPath = targetPath.Replace(rootpath, "");

                    targetPath = targetPath.Replace("\\", "/");

                    if (targetPath.StartsWith("/")) targetPath = "~" + targetPath;
                    else if (!targetPath.StartsWith("~")) targetPath = "~/" + targetPath;

                }

                if (targetPath.Contains("?") || targetPath.Contains("#") || targetPath.Contains(' ')) throw new Exception("nfCMS FileManagement does not accept '?' or '#' characters or white spaces in Path");
                if (targetPath.EndsWith("/"))
                {

                    targetPath = targetPath.Remove(targetPath.LastIndexOf("/"));

                }
                string[] pathSplitted = targetPath.Split('/');
                if (pathSplitted.Length > 0)
                {
                    string lastDir = pathSplitted[0];

                    if (pathSplitted.Length > 1)
                    {

                        for (int y = 1; y < pathSplitted.Length; y++)
                        {

                            lastDir = lastDir + "/" + pathSplitted[y];
                            if (!Directory.Exists(HttpContext.Current.Server.MapPath(lastDir)))
                            {

                                //Create Directory if not exists
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(lastDir));

                            }

                        }

                    }

                }

                Security.CryptoServices CS = new Security.CryptoServices();

                string targetFileName = DateTime.Now + Path.GetFileNameWithoutExtension(path);
                string extens = (String.IsNullOrEmpty(Path.GetExtension(path)) ? ".unknown" : Path.GetExtension(path));

                targetFileName = CS.ConvertToSHA1(targetFileName) + extens;
                targetPath = targetPath + "/" + targetFileName;

                Filep.filepath = targetPath;
                bool fileIsOk = false;
                if (allowedExtensions != null)
                {

                    if (allowedExtensions.Length == 0) fileIsOk = true;
                    else
                    {

                        foreach (string ext in allowedExtensions)
                        {

                            if (ext.ToLower() == Path.GetExtension(path.ToLower())) fileIsOk = true;

                        }

                    }

                }
                else
                {

                    fileIsOk = true;
                }

                //File is safe
                bool uploadOK = false;
                if (fileIsOk)
                {

                    HttpPostedFile Sv = HttpContext.Current.Request.Files[postFieldName];
                    Sv.SaveAs(HttpContext.Current.Server.MapPath(targetPath));
                    // @File.Copy(path, targetPath);

                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(targetPath)))
                    {

                        uploadOK = true;

                        string query = "INSERT INTO " + TA.getSqlPrefix + "Files (fpath, aliasName, needPermission, fileSize, active, ProfileID) VALUES(@fpath,@aliasName,@allow2groups,@fsize,1,@ProfileID)";
                        string aliasName = (String.IsNullOrEmpty(Filep.aliasName) ? Path.GetFileName(path) : Filep.aliasName);
                        string allow2groups = "";

                        if (String.IsNullOrEmpty(Filep.needPermission)) Filep.needPermission = "";
                        SqlPara.Add("@fsize", Sv.ContentLength);
                        SqlPara.Add("@fpath", targetPath);
                        SqlPara.Add("@aliasName", aliasName);
                        SqlPara.Add("@allow2groups", Filep.needPermission);
                        if (Filep.ProfileID < 1) Filep.ProfileID = 1;
                        SqlPara.Add("@ProfileID", Filep.ProfileID);

                        Sql.SysConnect();

                        Sql.SysNonQuery(query, SqlPara);

                        Sql.SysDisconnect();
                        //Register Extension / Refresh Extension Registration
                        this._registerExtension(Path.GetExtension(Sv.FileName), Sv.ContentType);

                    }

                }

            }
        }

        private void _registerFile(nFile Filep, HttpPostedFileBase fileBase, string[] allowedExtensions = null)
        {
            if (!String.IsNullOrEmpty(Filep.filepath))
            {

                SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
                SqlHelper.nSqlParameterCollection SqlPara = new SqlHelper.nSqlParameterCollection();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                //Handle the File

                string path = fileBase.FileName;
                path = HttpContext.Current.Server.MapPath(path);

                //Generate Filename

                string targetPath = Filep.filepath;
                if (targetPath.Contains("\\"))
                {

                    string rootpath = HttpContext.Current.Server.MapPath("~/");
                    //Get Mappath to the Root

                    targetPath = targetPath.Replace(rootpath, "");

                    targetPath = targetPath.Replace("\\", "/");

                    if (targetPath.StartsWith("/")) targetPath = "~" + targetPath;
                    else if (!targetPath.StartsWith("~")) targetPath = "~/" + targetPath;

                }

                if (targetPath.Contains("?") || targetPath.Contains("#") || targetPath.Contains(' ')) throw new Exception("nfCMS FileManagement does not accept '?' or '#' characters or white spaces in Path");
                if (targetPath.EndsWith("/"))
                {

                    targetPath = targetPath.Remove(targetPath.LastIndexOf("/"));

                }
                string[] pathSplitted = targetPath.Split('/');
                if (pathSplitted.Length > 0)
                {
                    string lastDir = pathSplitted[0];

                    if (pathSplitted.Length > 1)
                    {

                        for (int y = 1; y < pathSplitted.Length; y++)
                        {

                            lastDir = lastDir + "/" + pathSplitted[y];
                            if (!Directory.Exists(HttpContext.Current.Server.MapPath(lastDir)))
                            {
                                try
                                {
                                    //Create Directory if not exists
                                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(lastDir));
                                }
                                catch { }
                            }

                        }

                    }

                }

                Security.CryptoServices CS = new Security.CryptoServices();

                string targetFileName = DateTime.Now + Path.GetFileNameWithoutExtension(path);
                string extens = (String.IsNullOrEmpty(Path.GetExtension(path)) ? ".unknown" : Path.GetExtension(path));

                targetFileName = CS.ConvertToSHA1(targetFileName) + extens;
                targetPath = targetPath + "/" + targetFileName;

                Filep.filepath = targetPath;
                bool fileIsOk = false;
                if (allowedExtensions != null)
                {

                    if (allowedExtensions.Length == 0) fileIsOk = true;
                    else
                    {

                        foreach (string ext in allowedExtensions)
                        {

                            if (ext.ToLower() == Path.GetExtension(path.ToLower())) fileIsOk = true;

                        }

                    }

                }
                else
                {

                    fileIsOk = true;
                }

                //File is safe
                bool uploadOK = false;
                if (fileIsOk)
                {

                    fileBase.SaveAs(HttpContext.Current.Server.MapPath(targetPath));
                    // @File.Copy(path, targetPath);

                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(targetPath)))
                    {

                        uploadOK = true;

                        string query = "INSERT INTO " + TA.getSqlPrefix + "Files (fpath, aliasName, needPermission, fileSize, active, ProfileID) VALUES(@fpath,@aliasName,@allow2groups,@fsize,1,@ProfileID)";
                        string aliasName = (String.IsNullOrEmpty(Filep.aliasName) ? Path.GetFileName(path) : Filep.aliasName);
                        string allow2groups = "";

                        if (String.IsNullOrEmpty(Filep.needPermission)) Filep.needPermission = "";
                        SqlPara.Add("@fsize", fileBase.ContentLength);
                        SqlPara.Add("@fpath", targetPath);
                        SqlPara.Add("@aliasName", aliasName);
                        SqlPara.Add("@allow2groups", Filep.needPermission);
                        if (Filep.ProfileID < 1) Filep.ProfileID = 1;
                        SqlPara.Add("@ProfileID", Filep.ProfileID);

                        Sql.SysConnect();

                        Sql.SysNonQuery(query, SqlPara);

                        Sql.SysDisconnect();
                        //Register Extension / Refresh Extension Registration
                        this._registerExtension(Path.GetExtension(fileBase.FileName), fileBase.ContentType);

                    }

                }

            }
        }

        private void _unregisterFile(string aliasFileName)
        {
            SqlHelper.SqlHelper Sql = new SqlHelper.SqlHelper();
            ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();

            Sql.SysConnect();

            string query = "SELECT fpath FROM " + TA.getSqlPrefix + "Files WHERE aliasName=@name";
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

            PCOL.Add("@name", aliasFileName);
            SqlDataReader R = Sql.SysReader(query, PCOL);
            if (R.HasRows)
            {
                R.Read();

                string fp = R["fpath"].ToString();
                fp = HttpContext.Current.Server.MapPath(fp);
                System.IO.File.Delete(fp);

            }

            R.Close();

            SqlHelper.nSqlParameterCollection PCOL2 = new SqlHelper.nSqlParameterCollection();

            PCOL2.Add("@aname", aliasFileName);
            string delete = "DELETE " + TA.getSqlPrefix + "Files WHERE aliasName=@aname";
            Sql.SysNonQuery(delete, PCOL2);

            Sql.SysDisconnect();
        }

        #endregion Methods

        #region Nested Types

        public partial class FilemanagementControllers
        {
            #region Fields

            private List<string> _acceptedMimeTypes = new List<string>();
            private List<string> _acceptedProfiles = new List<string>();
            private int _id = 0;

            #endregion Fields

            #region Constructors

            public FilemanagementControllers(string controllerName)
            {
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                string pref = TA.getSqlPrefix;
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                SQL.SysConnect();

                string query = "SELECT id FROM " + pref + "FilemanagementControllers WHERE ControllerName=@name";
                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

                PCOL.Add("@name", controllerName);
                int id = 0;

                SqlDataReader R = SQL.SysReader(query, PCOL);

                if (R.HasRows)
                {

                    while (R.Read())
                    {

                        id = (int)R["id"];

                    }

                }

                R.Close();

                if (id != 0)
                {

                    string profiles = "SELECT p.ProfileName as ProfileName FROM " + pref + "FileManagementProfiles p INNER JOIN " + pref + "FilemanagementControllersAcceptProfiles a ON(a.pid = p.id) WHERE a.cid=@id";
                    SqlHelper.nSqlParameterCollection PCOl2 = new SqlHelper.nSqlParameterCollection();

                    PCOl2.Add("@id", id);

                    SqlDataReader P = SQL.SysReader(profiles, PCOl2);
                    while (P.Read())
                    {

                        this._acceptedProfiles.Add((string)P["ProfileName"]);

                    }
                    P.Close();

                    string types = "SELECT MimeType FROM " + pref + "FilemanagementControllersAcceptMimeTypes WHERE cid=@id";
                    SqlHelper.nSqlParameterCollection PCOL3 = new SqlHelper.nSqlParameterCollection();
                    PCOL3.Add("@id", id);
                    SqlDataReader M = SQL.SysReader(types, PCOL3);
                    if (M.HasRows)
                    {
                        while (M.Read())
                        {
                            this._acceptedMimeTypes.Add((string)M["MimeType"]);
                        }
                    }
                    M.Close();

                }

                SQL.SysDisconnect();
                this._id = id;
            }

            #endregion Constructors

            #region Methods

            public void addIfNotAcceptedMime(string mime, int controllerID)
            {
                if (this.mimeIsAccepted(mime, controllerID))
                {
                    SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                    ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                    string prefix = TA.getSqlPrefix;

                    SqlHelper.nSqlParameterCollection PP = new SqlHelper.nSqlParameterCollection();
                    PP.Add("@cid", controllerID);
                    PP.Add("@mime", mime);

                    string query = "INSERT INTO " + prefix + "FilemanagementControllersAcceptMimeTypes (MimeType,cid) VALUES(@mime, @cid)";

                    SQL.SysConnect();

                    SQL.SysNonQuery(query, PP);
                    SQL.SysDisconnect();
                }
            }

            public void addIfNotAcceptedProfile(string profileName, int controllerID)
            {
                nFileProfiles Prof = new nFileProfiles(profileName);
                if (Prof.ID > 0 && !this.profileIsAccepted(profileName, controllerID))
                {

                    SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                    ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                    string prefix = TA.getSqlPrefix;

                    SqlHelper.nSqlParameterCollection PP = new SqlHelper.nSqlParameterCollection();
                    PP.Add("@cid", controllerID);
                    PP.Add("@pid", Prof.ID);

                    string query = "INSERT INTO " + prefix + "FilemanagementControllersAcceptProfiles (pid,cid) VALUES(@pid, @cid)";

                    SQL.SysConnect();

                    SQL.SysNonQuery(query, PP);
                    SQL.SysDisconnect();
                }
            }

            public bool ControllerExists(string controllerName)
            {
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                string pref = TA.getSqlPrefix;
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                SQL.SysConnect();

                string query = "SELECT id FROM " + pref + "FilemanagementControllers WHERE ControllerName=@name";
                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

                PCOL.Add("@name", controllerName);
                int id = 0;

                SqlDataReader R = SQL.SysReader(query, PCOL);

                if (R.HasRows)
                {
                    R.Close();
                    SQL.SysDisconnect();
                    return true;
                }
                if (!R.IsClosed)
                {
                    R.Close();
                    SQL.SysDisconnect();
                    return false;

                }
                return false;
            }

            public bool FileIsAccepted(nFile FileEntry)
            {
                /*Profile
                 */

                //If NULL or 0  set to DEFAULT ID: 1
                int fakeProfileID = 1;

                if (FileEntry.ProfileID != null && FileEntry.ProfileID > 0)
                {
                    fakeProfileID = FileEntry.ProfileID;

                }

                nFileProfiles Prof = new nFileProfiles(fakeProfileID);

                /*MimeType
                 */
                string MIMETYPE = FileEntry.mimetype;

                //Query Prof
                IEnumerable<string> ProfileNames = from profname in this._acceptedProfiles
                                                   where profname == Prof.ProfileName

                                                   select profname;

                bool profile_accepted = false;
                foreach (string found in ProfileNames)
                {
                    profile_accepted = true;
                    break;
                }

                //Query MIME

                IEnumerable<string> MIMES = from mimex in this._acceptedMimeTypes where mimex.ToLower() == FileEntry.mimetype.ToLower() || mimex.Contains('*') select mimex;
                bool mime_accepted = false;
                foreach (string mim in MIMES)
                {
                    if (mim.Contains('*'))
                    {

                        if (mim == "*") mime_accepted = true;
                        else
                        {

                            string xmi = mim.Remove(mim.LastIndexOf('*'));
                            if (xmi.Length <= mim.Length)
                            {
                                if (xmi == mim.Substring(0, xmi.Length))
                                {
                                    mime_accepted = true;

                                }
                            }
                        }

                    }
                    else
                        mime_accepted = true;
                    break;

                }

                if (mime_accepted && profile_accepted) return true;
                else return false;
            }

            public int getID()
            {
                return this._id;
            }

            public int registerFilemanagementController(string controllername)
            {
                if (this.ControllerExists(controllername))
                {

                    return -1;
                }
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                SqlHelper.nSqlParameterCollection SPP = new SqlHelper.nSqlParameterCollection();
                SPP.Add("@name", controllername);
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                string prefix = TA.getSqlPrefix;

                string nonQuery = "INSERT INTO " + prefix + "FilemanagementControllers (ControllerName) VALUES(@name)";
                SQL.SysConnect();
                SQL.SysNonQuery(nonQuery, SPP);
                int newID = SQL.getLastId(prefix + "FilemanagementControllers");
                SQL.SysDisconnect();
                this._id = newID;
                return newID;
            }

            private bool mimeIsAccepted(string mime, int controllerID)
            {
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                string prefix = TA.getSqlPrefix;

                SqlHelper.nSqlParameterCollection PP = new SqlHelper.nSqlParameterCollection();
                PP.Add("@cid", controllerID);
                PP.Add("@mime", mime);

                string query = "SELECT * FROM " + prefix + "FilemanagementControllersAcceptMimeTypes WHERE MimeType=@mime AND cid=@cid";
                SQL.SysConnect();

                SqlDataReader R = SQL.SysReader(query, PP);
                bool exists = false;
                if (R.HasRows)
                {

                    exists = true;
                }
                R.Close();

                SQL.SysDisconnect();

                return exists;
            }

            private bool profileIsAccepted(string profileName, int controllerID)
            {
                nFileProfiles Prof = new nFileProfiles(profileName);
                if (Prof.ID > 0)
                {
                    SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                    ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                    string prefix = TA.getSqlPrefix;

                    SqlHelper.nSqlParameterCollection PP = new SqlHelper.nSqlParameterCollection();
                    PP.Add("@cid", controllerID);
                    PP.Add("@pid", Prof.ID);

                    string query = "SELECT * FROM " + prefix + "FilemanagementControllersAcceptProfiles WHERE pid=@pid AND cid=@cid";
                    SQL.SysConnect();

                    SqlDataReader R = SQL.SysReader(query, PP);
                    bool exists = false;
                    if (R.HasRows)
                    {

                        exists = true;
                    }
                    R.Close();

                    SQL.SysDisconnect();

                    return exists;

                }

                return true;
            }

            #endregion Methods
        }

        public partial class nFileProfileManagement
        {
            #region Methods

            public int createProfile(string name, List<FileSettingModel> settings)
            {
                nFileProfiles Prof = new nFileProfiles(name);

                if (Prof.ID == 0)
                {

                    SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                    ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                    SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

                    PCOL.Add("@name", name);

                    string query = "INSERT INTO " + TA.getSqlPrefix + "FileManagementProfiles (ProfileName) VALUES(@name)";
                    SQL.SysConnect();
                    SQL.SysNonQuery(query, PCOL);

                    SQL.SysDisconnect();

                    Prof = new nFileProfiles(name);

                }
                else
                {
                    throw new Exception("Profile Name allready exists");

                }
                foreach (FileSettingModel Setting in settings)
                {

                    if (!this.settingExists(Setting.Name))
                    {
                        this.addSetting(Prof.ID, Setting.Name, Setting.Value);
                    }
                    else
                    {
                        FileSettingModel temp = Prof.getProfileSetting(Setting.Name);

                        if (this.valueExists(temp.ID, Prof.ID))
                            this.changeValue(temp.ID, Setting.Name, Setting.Value);
                        else
                            this.addValue(Prof.ID, temp.ID, Setting.Value);
                    }

                }

                return Prof.ID;
            }

            public void deleteProfile(int profileID)
            {
                this.delete_profile(profileID);
            }

            public void setSetting(int profileID, string settingName, string value)
            {
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                SQL.SysConnect();

                int id = 0;

                string query = "SELECT id FROM " + TA.getSqlPrefix + "FileManagementFileSettings WHERE SettingName=@name";
                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
                PCOL.Add("@name", settingName);

                SqlDataReader R = SQL.SysReader(query, PCOL);
                if (R.HasRows)
                {

                    R.Read();
                    id = (int)R["id"];

                }
                R.Close();
                SQL.SysDisconnect();
                if (id == 0) this.addSetting(profileID, settingName, value);
                else
                {
                    if (this.valueExists(id, profileID)) this.changeValue(profileID, settingName, value);
                    else this.addValue(profileID, id, value);
                }
            }

            public void setSetting(int profileID, int settingID, string value)
            {
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                SQL.SysConnect();

                string settingName = "";

                string query = "SELECT SettingName FROM " + TA.getSqlPrefix + "FileManagementFileSettings WHERE id=@id";
                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
                PCOL.Add("@id", settingID);

                SqlDataReader R = SQL.SysReader(query, PCOL);
                if (R.HasRows)
                {

                    R.Read();
                    settingName = (string)R["SettingName"];

                }
                R.Close();
                SQL.SysDisconnect();
                if (settingName == "") throw new Exception("Setting does not exists");

                if (valueExists(settingID, profileID)) this.changeValue(profileID, settingName, value);
                else this.addValue(profileID, settingID, value);
            }

            public void setSetting(FileSettingModel Setting, int profileID)
            {
                if (this.settingExists(Setting.Name))
                {

                    if (this.valueExists(Setting.ID, profileID))
                    {
                        this.changeValue(profileID, Setting.Name, Setting.Value);

                    }
                    else
                    {
                        this.addValue(profileID, Setting.ID, Setting.Value);

                    }

                }
                else
                {
                    this.addSetting(profileID, Setting.Name, Setting.Value);

                }
            }

            public void updateProfile(int profileid, string newName)
            {
                this.update_profile(profileid, newName);
            }

            private void addSetting(int profileid, string name, string value = "")
            {
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();

                string query = "INSERT INTO " + TA.getSqlPrefix + "FileManagementFileSettings (SettingName)  VALUES(@name)";

                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

                PCOL.Add("@name", name);

                SQL.SysConnect();

                SQL.SysNonQuery(query, PCOL);

                int id = SQL.getLastId(TA.getSqlPrefix + "FileManagementFileSettings");

                string query2 = "INSERT INTO " + TA.getSqlPrefix + "FileManagementProfiles2FileSettings(ProfileID,SettingID) VALUES(@pid,@id)";
                SqlHelper.nSqlParameterCollection PCOl2 = new SqlHelper.nSqlParameterCollection();
                PCOl2.Add("@pid", profileid);
                PCOl2.Add("@id", id);

                SQL.SysNonQuery(query2, PCOl2);
                SQL.SysDisconnect();

                //Now the Value

                this.addValue(profileid, id, value);
            }

            private void addValue(int profileID, int settingID, string value)
            {
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                SQL.SysConnect();

                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                string query3 = "INSERT INTO " + TA.getSqlPrefix + "FileSettingValues (ProfileID,SettingID,SettingValue) VALUES(@pid,@id,@val)";
                SqlHelper.nSqlParameterCollection PCOl3 = new SqlHelper.nSqlParameterCollection();

                PCOl3.Add("@pid", profileID);
                PCOl3.Add("@id", settingID);
                PCOl3.Add("@val", value);
                SQL.SysNonQuery(query3, PCOl3);
            }

            private void changeValue(int profileID, string settingName, string newValue)
            {
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                SQL.SysConnect();

                string query = "UPDATE " + TA.getSqlPrefix + "FileSettingValues SET SettingValue=@val WHERE ProfileID=@pid AND SettingID=(SELECT id FROM " +
                                TA.getSqlPrefix + "FileManagementFileSettings WHERE SettingName=@name)";

                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

                PCOL.Add("@val", newValue);
                PCOL.Add("@pid", profileID);
                PCOL.Add("@name", settingName);

                SQL.SysNonQuery(query, PCOL);

                SQL.SysDisconnect();
            }

            private void delete_profile(int profileID)
            {
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                string pref = TA.getSqlPrefix;

                SQL.SysConnect();
                string query = "DELETE " + pref + "FileManagementProfiles WHERE id=@id";

                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

                PCOL.Add("@id", profileID);

                SQL.SysNonQuery(query, PCOL);
                SqlHelper.nSqlParameterCollection PCOL2 = new SqlHelper.nSqlParameterCollection();

                PCOL2.Add("@id", profileID);
                string query2 = "DELETE " + pref + "FileManagementProfiles2FileSettings WHERE ProfileID=@id";

                SQL.SysNonQuery(query2, PCOL2);

                SQL.SysDisconnect();
            }

            private bool settingExists(string name)
            {
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();

                string query = "SELECT * FROM " + TA.getSqlPrefix + "FileManagementFileSettings WHERE SettingName=@name";
                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
                SQL.SysConnect();

                PCOL.Add("@name", name);
                SqlDataReader R = SQL.SysReader(query, PCOL);
                bool ret = R.HasRows;
                R.Close();
                SQL.SysDisconnect();

                return ret;
            }

            private void update_profile(int profileID, string newName)
            {
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                string pref = TA.getSqlPrefix;

                SQL.SysConnect();
                string query = "UPDATE " + pref + "FileManagementProfiles SET ProfileName=@name WHERE id=@id";

                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
                PCOL.Add("@name", newName);
                PCOL.Add("@id", profileID);

                SQL.SysNonQuery(query, PCOL);

                SQL.SysDisconnect();
            }

            private bool valueExists(int settingID, int profileID)
            {
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                string query = "SELECT * FROM " + TA.getSqlPrefix + "FileSettingValues WHERE ProfileID=@profid AND SettingID=@settid";
                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
                SQL.SysConnect();
                PCOL.Add("@profid", profileID);
                PCOL.Add("@settid", settingID);

                SqlDataReader R = SQL.SysReader(query, PCOL);
                bool ret = R.HasRows;
                R.Close();
                SQL.SysDisconnect();

                return ret;
            }

            #endregion Methods
        }

        public partial class nFileProfiles
        {
            #region Fields

            private DataTable settings = new DataTable();
            private int _id = 0;
            private string _profilename = "";

            #endregion Fields

            #region Constructors

            public nFileProfiles(int profileID)
            {
                this._init(profileID);
            }

            public nFileProfiles(string profileName)
            {
                int id = 0;

                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                string pref = TA.getSqlPrefix;

                SQL.SysConnect();

                string query = "SELECT id FROM " + pref + "FileManagementProfiles WHERE ProfileName=@name";
                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

                PCOL.Add("@name", profileName);
                SqlDataReader R = SQL.SysReader(query, PCOL);

                if (R.HasRows)
                {

                    while (R.Read())
                    {

                        id = (int)R["id"];

                    }

                }

                R.Close();
                SQL.SysDisconnect();

                this._init(id);
            }

            #endregion Constructors

            #region Properties

            public int ID
            {
                get
                {

                    return this._id;

                }
            }

            public string ProfileName
            {
                get
                {
                    return this._profilename;
                }
            }

            #endregion Properties

            #region Methods

            public FileSettingModel getProfileSetting(string settingName)
            {
                FileSettingModel MDL = new FileSettingModel();
                DataRow[] Row = this.settings.Select("SettingName='" + settingName + "'");
                if (Row.Length > 0)
                {
                    MDL.Name = Row[0]["Name"].ToString();
                    MDL.ID = (int)Row[0]["id"];
                    MDL.Value = (string)Row[0]["Value"];

                }
                else
                {

                }

                return MDL;
            }

            private void _init(int profileID)
            {
                settings.Columns.Add("id", typeof(int));
                settings.Columns.Add("SettingName", typeof(string));
                settings.Columns.Add("Value", typeof(string));
                ThisApplication.ThisApplication TA = new ThisApplication.ThisApplication();
                string prefix = TA.getSqlPrefix;
                SqlHelper.SqlHelper SQL = new SqlHelper.SqlHelper();

                SQL.SysConnect();
                string query = "SELECT * FROM " + prefix + "FileManagementProfiles WHERE id=@id";
                SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
                PCOL.Add("@id", profileID);
                SqlDataReader MainRow = SQL.SysReader(query, PCOL);

                if (MainRow.HasRows)
                {
                    MainRow.Read();

                    this._profilename = (string)MainRow["ProfileName"];
                    this._id = (int)MainRow["id"];

                }

                MainRow.Close();

                if (this._profilename != "")
                {
                    string getSettings = "SELECT s.id as SettingID, s.SettingName as SettingName, v.SettingValue as SettingValue FROM " + prefix + "FileManagementFileSettings s INNER JOIN " + prefix + "FileManagementProfiles2FileSettings p ON(s.id = p.SettingID)" +
                                         " INNER JOIN " + prefix + "FileSettingValues v ON(s.id = v.SettingID) WHERE p.ProfileID = @id AND v.ProfileID=@id";

                    SqlHelper.nSqlParameterCollection PCOL2 = new SqlHelper.nSqlParameterCollection();
                    PCOL2.Add("@id", profileID);

                    SqlDataReader row = SQL.SysReader(getSettings, PCOL2);

                    if (row.HasRows)
                    {

                        while (row.Read())
                        {
                            this.settings.Rows.Add((int)row["SettingID"], (string)row["SettingName"], (string)row["SettingValue"]);

                        }

                    }
                    row.Close();
                }
            }

            #endregion Methods
        }

        #endregion Nested Types
    }

    //TODO: Controller für Userbilder
    public class FileSettingModel
    {
        #region Properties

        public int ID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Value
        {
            get; set;
        }

        #endregion Properties
    }

    public class nFile
    {
        #region Properties

        public string aliasName
        {
            get; set;
        }

        public string filepath
        {
            get; set;
        }

        public int id
        {
            get; set;
        }

        public bool isActive
        {
            get; set;
        }

        public string mimetype
        {
            get; set;
        }

        public string needPermission
        {
            get; set;
        }

        public int ProfileID
        {
            get; set;
        }

        #endregion Properties
    }
}