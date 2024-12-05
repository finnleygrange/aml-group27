# Choosing Database Technology for AML System  

## Context and Problem Statement  

The Advanced Media Library (AML) requires a database system to manage its vast collection of books, journals, multimedia, and other media resources, as well as user profiles, borrowing history, financial transactions, and branch-specific data. The system must:  
1. Provide 24/7 availability for online services while supporting in-branch and phone interactions during operational hours.  
2. Maintain real-time synchronization between branches to ensure seamless borrowing and returning across the national network.  
3. Scale with AML’s projected user base growth (10% annually) to avoid performance bottlenecks.  
4. Handle structured data (e.g., user accounts, transactions) and semi-structured data (e.g., user feedback).  
5. Ensure data integrity, security, and compliance with GDPR and accessibility standards.  

The decision is crucial as the database forms the backbone of AML’s new library management system, directly impacting reliability, scalability, and user experience.  

## Decision Drivers  

1. **Scalability**: The database must handle a growing user base, large datasets, and increased transactions without compromising performance.  
2. **Reliability**: The system must guarantee data accuracy and availability during peak usage.  
3. **Cost-effectiveness**: A balance between upfront costs, maintenance, and long-term scalability.  
4. **Team Familiarity**: The chosen solution must align with the team's existing skills to minimize onboarding and development delays.  
5. **Integration**: The database must support integration with the AML mobile app, web platform, and branch systems.  

## Considered Options  

1. **Relational Database (PostgreSQL)**  
2. **NoSQL Database (MongoDB)**  
3. **Hybrid Solution (AWS Aurora)**  

## Decision Outcome  

### Chosen Option: **Relational Database (PostgreSQL)**  

PostgreSQL was selected as the database solution for AML based on its alignment with AML's requirements for transactional consistency, scalability, and ease of use for structured data.  

#### Key Reasons for Selection  
- **Scalability:** PostgreSQL’s horizontal scaling via partitioning and replication supports AML’s projected growth.  
- **Reliability:** ACID (Atomicity, Consistency, Isolation, Durability) compliance ensures data consistency for critical operations like borrowing, reserving, and returning media.  
- **Cost-Effectiveness:** PostgreSQL is open-source, reducing licensing costs, and offers extensive plugins and features to minimize development efforts.  
- **Flexibility:** PostgreSQL can handle both structured and some semi-structured data (JSONB support).  
- **Team Familiarity:** The development team’s existing SQL expertise facilitates a faster implementation timeline.  

### Rejected Options  

#### **MongoDB**  
- **Strengths:**  
  - Excellent for semi-structured and unstructured data.  
  - High write performance, useful for real-time updates.  
- **Weaknesses:**  
  - Lack of native transactional support for complex relationships between entities (e.g., user borrowing history linked to multiple branches).  
  - Requires extensive effort to ensure data integrity for financial transactions.  

#### **AWS Aurora**  
- **Strengths:**  
  - Combines the scalability of cloud-native databases with the structured querying of relational systems.  
  - Automated management features reduce operational overhead.  
- **Weaknesses:**  
  - Vendor lock-in increases long-term costs and risks dependency.  
  - Higher costs compared to PostgreSQL and MongoDB.  

## Consequences  

### Positive Outcomes  
- **Improved Data Integrity:** Consistency and reliability of transactions across all branches.  
- **Seamless Integration:** Smooth operation across AML’s web, mobile, and in-branch services.  
- **Cost Savings:** Open-source solution reduces development and operational expenses.  
- **Future-Proofing:** PostgreSQL’s extensibility allows for incremental enhancements.  

### Negative Outcomes  
- **Complex Setup for Scaling:** Requires additional configuration (e.g., sharding, replication) to support extremely high traffic.  
- **Latency Considerations:** While adequate for AML’s needs, write performance may lag behind NoSQL in scenarios involving very high concurrency.  

## Confirmation  

The decision will be validated through:  
1. **Proof-of-Concept (PoC):**  
   - Implementing core AML functionalities like media search, borrowing, and returning.  
   - Simulating user growth to test system performance under peak loads.  
2. **Testing:**  
   - Functional testing to ensure proper handling of transactions.  
   - Performance benchmarking with projected user growth and branch operations.  
   - Compliance checks for GDPR and accessibility standards.  
3. **Feedback Loop:** Regular testing and review cycles during the sprint to incorporate performance tuning and ensure architectural alignment.  

## Pros and Cons of the Options  

### Relational Database (PostgreSQL)  
**Pros:**  
- Mature, proven technology for transactional systems.  
- Strong ecosystem of tools and extensions.  
- Supports complex queries and relationships (e.g., join-heavy workloads).  
- ACID compliance ensures reliable data operations.  

**Cons:**  
- Requires configuration to scale horizontally.  
- Write performance may lag in very high-concurrency scenarios compared to NoSQL.  

### NoSQL Database (MongoDB)  
**Pros:**  
- Scales horizontally with ease.  
- Optimized for high-velocity, unstructured data workloads.  
- Flexible schema design for evolving data models.  

**Cons:**  
- Limited support for complex queries.  
- Lacks ACID compliance for multi-document transactions by default.  

### Hybrid Solution (AWS Aurora)  
**Pros:**  
- Combines the best of relational and NoSQL features.  
- Fully managed service reduces operational burden.  

**Cons:**  
- Expensive for large-scale, long-term use.  
- Vendor lock-in limits flexibility in future transitions.  

## More Information  

This decision will be revisited after validating the PoC. Further evaluations will include exploring sharding configurations and implementing backup strategies to support AML's long-term growth.  
