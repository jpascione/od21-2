function patientportal(){var O='bootstrap',P='begin',Q='gwt.codesvr.patientportal=',R='gwt.codesvr=',S='patientportal',T='startup',U='DUMMY',V=0,W=1,X='iframe',Y='javascript:""',Z='position:absolute; width:0; height:0; border:none; left: -1000px;',$=' top: -1000px;',_='CSS1Compat',ab='<!doctype html>',bb='',cb='<html><head><\/head><body><\/body><\/html>',db='undefined',eb='DOMContentLoaded',fb=50,gb='Chrome',hb='eval("',ib='");',jb='script',kb='javascript',lb='moduleStartup',mb='moduleRequested',nb='Failed to load ',ob='head',pb='meta',qb='name',rb='patientportal::',sb='::',tb='gwt:property',ub='content',vb='=',wb='gwt:onPropertyErrorFn',xb='Bad handler "',yb='" for "gwt:onPropertyErrorFn"',zb='gwt:onLoadErrorFn',Ab='" for "gwt:onLoadErrorFn"',Bb='#',Cb='?',Db='/',Eb='img',Fb='clear.cache.gif',Gb='baseUrl',Hb='patientportal.nocache.js',Ib='base',Jb='//',Kb='mgwt.formfactor',Lb='formfactor',Mb='&',Nb='iphone',Ob='ipod',Pb='phone',Qb='ipad',Rb='tablet',Sb='android',Tb='mobile',Ub='desktop',Vb=2,Wb='user.agent',Xb='webkit',Yb='safari',Zb='msie',$b=10,_b=11,ac='ie10',bc=9,cc='ie9',dc=8,ec='ie8',fc='gecko',gc='gecko1_8',hc=3,ic=4,jc='selectingPermutation',kc='patientportal.devmode.js',lc='0A658BFE34A8035AD4ED24A34B0BBA08',mc='10A9E7D9302ACF27E61FB42AD32D9A12',nc='4A73197068326077847456931C0F55F0',oc='561379F00E02D0636804A986775BA4ED',pc='5D4D26A6AD5C039AED7BEEE11F53D86D',qc='60609814C2FB4AFB246BBF66AA5C422A',rc='61EEC1AD7034572F9B1A6DAEAA934FDA',sc='8A50904AF517AE53701760206239A304',tc='922FEE58987D3C43DBD82FB1D76FD516',uc='AFC66EFC0803295CF8C00097029EDBEB',vc='BD086DE33C9A9399E6FC77AF5CDB4FBE',wc='BF1DA8F81C306D7509BDDF8B7510223E',xc='CA98FE44DCD2886B5669E7ABBB39707E',yc='EEB5773E201C377166802D0C1A33A2D7',zc='F8D532A51CCB63D3D8D6A25036BCEE80',Ac=':',Bc='.cache.js',Cc='link',Dc='rel',Ec='stylesheet',Fc='href',Gc='loadExternalRefs',Hc='gwt/clean/clean.css',Ic='end',Jc='http:',Kc='file:',Lc='_gwt_dummy_',Mc='__gwtDevModeHook:patientportal',Nc='Ignoring non-whitelisted Dev Mode URL: ',Oc=':moduleBase';var o=window;var p=document;r(O,P);function q(){var a=o.location.search;return a.indexOf(Q)!=-1||a.indexOf(R)!=-1}
function r(a,b){if(o.__gwtStatsEvent){o.__gwtStatsEvent({moduleName:S,sessionId:o.__gwtStatsSessionId,subSystem:T,evtGroup:a,millis:(new Date).getTime(),type:b})}}
patientportal.__sendStats=r;patientportal.__moduleName=S;patientportal.__errFn=null;patientportal.__moduleBase=U;patientportal.__softPermutationId=V;patientportal.__computePropValue=null;patientportal.__getPropMap=null;patientportal.__installRunAsyncCode=function(){};patientportal.__gwtStartLoadingFragment=function(){return null};patientportal.__gwt_isKnownPropertyValue=function(){return false};patientportal.__gwt_getMetaProperty=function(){return null};var s=null;var t=o.__gwt_activeModules=o.__gwt_activeModules||{};t[S]={moduleName:S};patientportal.__moduleStartupDone=function(e){var f=t[S].bindings;t[S].bindings=function(){var a=f?f():{};var b=e[patientportal.__softPermutationId];for(var c=V;c<b.length;c++){var d=b[c];a[d[V]]=d[W]}return a}};var u;function v(){w();return u}
function w(){if(u){return}var a=p.createElement(X);a.src=Y;a.id=S;a.style.cssText=Z+$;a.tabIndex=-1;p.body.appendChild(a);u=a.contentDocument;if(!u){u=a.contentWindow.document}u.open();var b=document.compatMode==_?ab:bb;u.write(b+cb);u.close()}
function A(k){function l(a){function b(){if(typeof p.readyState==db){return typeof p.body!=db&&p.body!=null}return /loaded|complete/.test(p.readyState)}
var c=b();if(c){a();return}function d(){if(!c){c=true;a();if(p.removeEventListener){p.removeEventListener(eb,d,false)}if(e){clearInterval(e)}}}
if(p.addEventListener){p.addEventListener(eb,d,false)}var e=setInterval(function(){if(b()){d()}},fb)}
function m(c){function d(a,b){a.removeChild(b)}
var e=v();var f=e.body;var g;if(navigator.userAgent.indexOf(gb)>-1&&window.JSON){var h=e.createDocumentFragment();h.appendChild(e.createTextNode(hb));for(var i=V;i<c.length;i++){var j=window.JSON.stringify(c[i]);h.appendChild(e.createTextNode(j.substring(W,j.length-W)))}h.appendChild(e.createTextNode(ib));g=e.createElement(jb);g.language=kb;g.appendChild(h);f.appendChild(g);d(f,g)}else{for(var i=V;i<c.length;i++){g=e.createElement(jb);g.language=kb;g.text=c[i];f.appendChild(g);d(f,g)}}}
patientportal.onScriptDownloaded=function(a){l(function(){m(a)})};r(lb,mb);var n=p.createElement(jb);n.src=k;if(patientportal.__errFn){n.onerror=function(){patientportal.__errFn(S,new Error(nb+code))}}p.getElementsByTagName(ob)[V].appendChild(n)}
patientportal.__startLoadingFragment=function(a){return D(a)};patientportal.__installRunAsyncCode=function(a){var b=v();var c=b.body;var d=b.createElement(jb);d.language=kb;d.text=a;c.appendChild(d);c.removeChild(d)};function B(){var c={};var d;var e;var f=p.getElementsByTagName(pb);for(var g=V,h=f.length;g<h;++g){var i=f[g],j=i.getAttribute(qb),k;if(j){j=j.replace(rb,bb);if(j.indexOf(sb)>=V){continue}if(j==tb){k=i.getAttribute(ub);if(k){var l,m=k.indexOf(vb);if(m>=V){j=k.substring(V,m);l=k.substring(m+W)}else{j=k;l=bb}c[j]=l}}else if(j==wb){k=i.getAttribute(ub);if(k){try{d=eval(k)}catch(a){alert(xb+k+yb)}}}else if(j==zb){k=i.getAttribute(ub);if(k){try{e=eval(k)}catch(a){alert(xb+k+Ab)}}}}}__gwt_getMetaProperty=function(a){var b=c[a];return b==null?null:b};s=d;patientportal.__errFn=e}
function C(){function e(a){var b=a.lastIndexOf(Bb);if(b==-1){b=a.length}var c=a.indexOf(Cb);if(c==-1){c=a.length}var d=a.lastIndexOf(Db,Math.min(c,b));return d>=V?a.substring(V,d+W):bb}
function f(a){if(a.match(/^\w+:\/\//)){}else{var b=p.createElement(Eb);b.src=a+Fb;a=e(b.src)}return a}
function g(){var a=__gwt_getMetaProperty(Gb);if(a!=null){return a}return bb}
function h(){var a=p.getElementsByTagName(jb);for(var b=V;b<a.length;++b){if(a[b].src.indexOf(Hb)!=-1){return e(a[b].src)}}return bb}
function i(){var a=p.getElementsByTagName(Ib);if(a.length>V){return a[a.length-W].href}return bb}
function j(){var a=p.location;return a.href==a.protocol+Jb+a.host+a.pathname+a.search+a.hash}
var k=g();if(k==bb){k=h()}if(k==bb){k=i()}if(k==bb&&j()){k=e(p.location.href)}k=f(k);return k}
function D(a){if(a.match(/^\//)){return a}if(a.match(/^[a-zA-Z]+:\/\//)){return a}return patientportal.__moduleBase+a}
function F(){var g=[];var h=V;function i(a,b){var c=g;for(var d=V,e=a.length-W;d<e;++d){c=c[a[d]]||(c[a[d]]=[])}c[a[e]]=b}
var j=[];var k=[];function l(a){var b=k[a](),c=j[a];if(b in c){return b}var d=[];for(var e in c){d[c[e]]=e}if(s){s(a,d,b)}throw null}
k[Kb]=function(){var a=location.search;var b=a.indexOf(Lb);if(b>=V){var c=a.substring(b);var d=c.indexOf(vb)+W;var e=c.indexOf(Mb);if(e==-1){e=c.length}return c.substring(d,e)}var f=navigator.userAgent.toLowerCase();if(f.indexOf(Nb)!=-1||f.indexOf(Ob)!=-1){return Pb}else if(f.indexOf(Qb)!=-1){return Rb}else if(f.indexOf(Sb)!=-1){if(f.indexOf(Tb)!=-1){return Pb}else{return Rb}}return Ub};j[Kb]={'desktop':V,'phone':W,'tablet':Vb};k[Wb]=function(){var a=navigator.userAgent.toLowerCase();var b=p.documentMode;if(function(){return a.indexOf(Xb)!=-1}())return Yb;if(function(){return a.indexOf(Zb)!=-1&&(b>=$b&&b<_b)}())return ac;if(function(){return a.indexOf(Zb)!=-1&&(b>=bc&&b<_b)}())return cc;if(function(){return a.indexOf(Zb)!=-1&&(b>=dc&&b<_b)}())return ec;if(function(){return a.indexOf(fc)!=-1||b>=_b}())return gc;return bb};j[Wb]={'gecko1_8':V,'ie10':W,'ie8':Vb,'ie9':hc,'safari':ic};__gwt_isKnownPropertyValue=function(a,b){return b in j[a]};patientportal.__getPropMap=function(){var a={};for(var b in j){if(j.hasOwnProperty(b)){a[b]=l(b)}}return a};patientportal.__computePropValue=l;o.__gwt_activeModules[S].bindings=patientportal.__getPropMap;r(O,jc);if(q()){return D(kc)}var m;try{i([Rb,ec],lc);i([Rb,ac],mc);i([Pb,ac],nc);i([Pb,cc],oc);i([Ub,cc],pc);i([Rb,cc],qc);i([Ub,ec],rc);i([Ub,Yb],sc);i([Pb,ec],tc);i([Rb,Yb],uc);i([Rb,gc],vc);i([Ub,ac],wc);i([Pb,Yb],xc);i([Pb,gc],yc);i([Ub,gc],zc);m=g[l(Kb)][l(Wb)];var n=m.indexOf(Ac);if(n!=-1){h=parseInt(m.substring(n+W),$b);m=m.substring(V,n)}}catch(a){}patientportal.__softPermutationId=h;return D(m+Bc)}
function G(){if(!o.__gwt_stylesLoaded){o.__gwt_stylesLoaded={}}function c(a){if(!__gwt_stylesLoaded[a]){var b=p.createElement(Cc);b.setAttribute(Dc,Ec);b.setAttribute(Fc,D(a));p.getElementsByTagName(ob)[V].appendChild(b);__gwt_stylesLoaded[a]=true}}
r(Gc,P);c(Hc);r(Gc,Ic)}
B();patientportal.__moduleBase=C();t[S].moduleBase=patientportal.__moduleBase;var H=F();if(o){var I=!!(o.location.protocol==Jc||o.location.protocol==Kc);o.__gwt_activeModules[S].canRedirect=I;function J(){var b=Lc;try{o.sessionStorage.setItem(b,b);o.sessionStorage.removeItem(b);return true}catch(a){return false}}
if(I&&J()){var K=Mc;var L=o.sessionStorage[K];if(!/^http:\/\/(localhost|127\.0\.0\.1)(:\d+)?\/.*$/.test(L)){if(L&&(window.console&&console.log)){console.log(Nc+L)}L=bb}if(L&&!o[K]){o[K]=true;o[K+Oc]=C();var M=p.createElement(jb);M.src=L;var N=p.getElementsByTagName(ob)[V];N.insertBefore(M,N.firstElementChild||N.children[V]);return false}}}G();r(O,Ic);A(H);return true}
patientportal.succeeded=patientportal();