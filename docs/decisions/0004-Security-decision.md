# ADR-4: Choosing Authentication Mechanism for Web Application

## Status
Proposed

## Context
We need to implement user authentication for our web application within a limited time frame. The original plan was to implement JWT  for stateless authentication, which would allow for scalability and SSO capabilities across services. However, due to time constraints and the complexity of implementing JWT, we need to choose a simpler, quicker solution.

### The alternatives considered include:

-Session-based authentication: A traditional approach where the server stores session information and sends a session ID to the client.
-Basic authentication: A simpler method that uses HTTP headers to send the username and password for each request (though less secure).
-JWT: Initially the preferred choice, but rejected due to time limitations.

## Decision
We decided to implement Session-based Authentication for the following reasons:

-Quick to implement: This is a standard, well-understood method with built-in support in most backend frameworks.
-Less complexity: Unlike JWT, session management doesn't require token generation or handling token expiration, making it easier to implement in a short time.
-Security: Session-based authentication can still be secure when using techniques like HttpOnly cookies to store the session ID, reducing the risk of cross-site scripting (XSS).

## Consequences
-Scalability: While session-based authentication is easier to implement, it requires maintaining session state on the server, which can be more challenging as the application scales. We will need to implement load balancing and session sharing (e.g., using Redis or sticky sessions) to handle scaling.
-Performance: Server-side session management adds some overhead for storing and retrieving session data, but this is acceptable for the project's current scale.
-Security considerations: We will implement secure cookies (HttpOnly and Secure flags) to mitigate risks associated with session hijacking.


## Alternatives Considered
- JWT (JSON Web Tokens): Initially planned for its stateless nature and ability to support scalability, but rejected due to the time required for implementation and testing, especially around handling token expiration and refresh.
-Basic Authentication: Not suitable for production use due to security concerns, as credentials are sent with every request, potentially exposing sensitive data.
