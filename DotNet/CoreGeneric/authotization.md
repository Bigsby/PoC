# Authorization

## Intro
Authorization is the name of the functionality its whole (sole) purpose is to determine if a resource can be accessed by who is requesting it.

## Players (Providers)
To achieve this "simple" purpose, there are 4 distinct players. Their responsibilities are, usually so mixed together, or included in the frameworks, that it's hard to tell who is who, who is doing what and, most importantly, who is (should be) supposed to do what.

The players are:
1. Identity Provider
   - **Purpose** - Identify single users, i.e., say: this is this user.
   - **Input** - Authentication token(s), e.g., username and password.
   - **Output** - User identifier or authenthication failed result. Some limmitted user descriptive data may also be provided, e.g., display name, avatar.
   - **Only knows** - Single users.
   - **Example** - An oAuth identity provider, e.g, SSO AD, Facebook, Google.
2. Membership Provider
   - **Purpose** - Map single users to organizational groups, i.e., say: this user is part of these groups
   - **Input** - User
   - **Output** - List of groups the user is assigned to, e.g., string array.
   - **Only knows** - Users and groups (roles).
   - **Example** - Active Directory (Federation) Roles
3. Authorization Provider
   - **Purpose** - Map organizational groups to resources, i.e., this resource can be accessed by this(ese) group(s).
   - **Input** - Resource identifier
   - **Output** - List of organizational groups that can access the resource, e.g., string array.
   - **Only knows** - Groups and resources.
   - **Example** - Authorize Attributes and/or Middleware in a ASP.Net Web API
4. Resource Provider
   - **Purpose** - Provide resources, if authorization is granted.
   - **Input** - Resource access request
   - **Output** - The resource.
   - **Only knows** - Resources.
   - **Example** - Any app that provides resources, with or without authorization, e.g., the Web API Faceook Graph, .

## Principles
When implementing or solving authorizaiton, it is paramount to, at everypoint, ask:
- What purpose is being fullfilled?
- How the purpose is being fullfilled?