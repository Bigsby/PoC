package controllers;

import play.mvc.*;
import play.libs.Json;

/**
 * This controller contains an action to handle HTTP requests
 * to the application's home page.
 */
public class HomeController extends Controller {

    /**
     * An action that renders an HTML page with a welcome message.
     * The configuration in the <code>routes</code> file means that
     * this method will be called when the application receives a
     * <code>GET</code> request with a path of <code>/</code>.
     */
    public Result index() {
        return ok(views.html.index.render());
    }

    public Result string() {
        return ok("This is just a string");
    }

    public Result json(){
        Person p = new Person();
        p.firstName = "John";
        p.lastName = "Cleese";
        return ok(Json.toJson(p));
    }

    public static class Person {
        public String firstName;
        public String lastName;
        public int age;
    }
}
