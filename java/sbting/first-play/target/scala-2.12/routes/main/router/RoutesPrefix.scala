// @GENERATOR:play-routes-compiler
// @SOURCE:C:/Git/Bigsby/PoC/java/sbting/first-play/conf/routes
// @DATE:Fri Aug 03 14:51:23 BST 2018


package router {
  object RoutesPrefix {
    private var _prefix: String = "/"
    def setPrefix(p: String): Unit = {
      _prefix = p
    }
    def prefix: String = _prefix
    val byNamePrefix: Function0[String] = { () => prefix }
  }
}
