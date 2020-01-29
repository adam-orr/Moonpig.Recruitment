# Moonpig Engineering recruitment test

Welcome to the Moonpig engineering test. Here at Moonpig we really value
quality code, and this test has been designed to allow you to show us how 
you think quality code should be written. 

To allow you to focus on the design and implementation of the code we have 
added all the use cases we expect you to implement to the bottom of the 
instructions. In return we ask that you make sure your implementation 
follows all the best practices you are aware of, and that at the end of it, 
the code you submit, is code you are proud of. 

We have not set a timelimit, we prefer that you spend some extra time to get
it right and write the highest quality code you can. Please feel free to make
any changes you want to the solution, add classes, remove projects etc. If you
change the request or response please update the example in the section below.

For bonus points, commit regularly and include the .git folder in your
submission. This will allow us to follow the evolution of your solution.

When complete please upload your solution and answers in a .zip to the google
drive link provided to you by the recruiter.

## Programming Exercise - Moonpig Post Office

You have been tasked with creating a service that calculates the estimated 
despatch dates of customers' orders. 

An order consists of an order date and a collection of products that a 
customer has added to their shopping basket. 

Each of these products is supplied to Moonpig on demand through a number of 
3rd party suppliers.

As soon as an order is received by a supplier, the supplier will start 
processing the order. The supplier has an agreed lead time in which to 
process the order before delivering it to the Moonpig Post Office.

Once the Moonpig Post Office has received all products in an order it is 
despatched to the customer.

**Assumptions**:

1. Suppliers start processing an order on the same day that the order is 
	received. For example, a supplier with a lead time of one day, receiving
	an order today will send it to Moonpig tomorrow.


2. For the purposes of this exercise we are ignoring time i.e. if a 
	supplier has a lead time of 1 day then an order received any time on 
	Tuesday would arrive at Moonpig on the Wednesday.

3. Once all products for an order have arrived at Moonpig from the suppliers, 
	they will be despatched to the customer on the same day.

### Part 1 

When the /api/DespatchDate endpoint is hit return the despatch date of that 
order.

### Part 2

Moonpig Post Office staff are getting complaints from customers expecting 
packages to be delivered on the weekend. You find out that the Moonpig post
office is shut over the weekend. Packages received from a supplier on a weekend 
will be despatched the following Monday.

Modify the existing code to ensure that any orders received from a supplier
on the weekend are despatched on the following Monday.

### Part 3

The Moonpig post office is still getting complaints... It turns out suppliers 
don't work during the weekend as well, i.e. if an order is received on the 
Friday with a lead time of 2 days, Moonpig would receive and despatch on the 
Tuesday.

Modify the existing code to ensure that any orders that would have been 
processed during the weekend resume processing on Monday.

---

Parts 1 & 2 have already been completed albeit lacking in quality. Please review
the code, document the problems you find (see question 1), and refactor into
what you would consider quality code. 

Once you have completed the refactoring, extend your solution to capture the 
requirements listed in part 3.

Please note, the provided DbContext is a stubbed class which provides test 
data. Please feel free to use this in your implementation and tests but do 
keep in mind that it would be switched for something like an EntityFramework 
DBContext backed by a real database in production.

While completing the exercise please answer the questions listed below. 
We are not looking for essay length answers. You can add the answers in this 
document.

## Questions

Q1. What 'code smells' / anti-patterns did you find in the existing 
	implemention of part 1 & 2?

Q2. What best practices have you used while implementing your solution?

Q3. What further steps would you take to improve the solution given more time?

Q4. What's a technology that you're excited about and where do you see this 
    being applicable? (Your answer does not have to be related to this problem)
	
## Answers
Q1.	* Unneccessary global declared variable of maxLeadTime
	* Unclear variable naming
	* Logic contained within the Controller and not separated into scalable services
	* No consistency in If Statement format i.e. one line, two lines with no brackets, mutli-line with brackets used
	* Assumption of Datebase Data in the Unit Tests
	* Using DateTime.Now in Unit Tests, when different logic applies depending on today's date
	* API reliant on the name of the Controller

Q2. * Interfaces - segregated so that only classes that implement their features use them
	* Abstracting core logic into seperate service, so that no logic resides in the controller itself. Also adding to scalability of the code
	* Creating services to handle a single responsibility
	* Refactor of conditionals to allow for concise code
	* Code structure consistency - equal spacing and formatting for similiar code (i.e. List creation, IF statements etc.)
	* Unit Testing using Mocked requests to reduce relying on database data
	* Dependency injection
	* Specified API path - to only allow for API calls to the given API to return the requested result
	* Extensions to DateTime object for future scalability
	* Comments for maintainability where code isn't necessarily as understandable
	* Naming variables so that they are understood to what they contain without checking the code flow

Q3. * As some suppliers may work on weekends, or even may not work on certain days of the week; additional data could be added to the database for each supplier to dictate which days they did/did not work and calculate an appropriate despatch date accordingly.
	* Add an API to allow adding and updating of Products and Suppliers into the database.
	* Add implementation to also take into account Bank Holidays when calculating the despatch date
	
Q4. What's a technology that you're excited about and where do you see this being applicable? (Your answer does not have to be related to this problem)
	* Artificial Intelligence - very versatile technology, can be used in countless different ways throughout software development. From machine learning to help solve complex problems to be able to predict a user's actions and providing the best possible experience. AI can be used in any industry and environment as it provides the tools available for the task that is needed.
	

## Request and Response Examples

Please see examples for how to make requests and the expected response below.

### Request

The service is setup as a Web API and takes a request in the following format

~~~~ 
GET /api/DespatchDate?ProductIds={product_id}&orderDate={order_date} 
~~~~

e.g.

~~~~ 
GET /api/DespatchDate?ProductIds=1&orderDate=2018-01-29T00:00:00
GET /api/DespatchDate?ProductIds=2&ProductIds=3&orderDate=2018-01-29T00:00:00 
~~~~

### Response

The response will be a JSON object with a date property set to the resulting 
Despatch Date

~~~~ 
{
    "date" : "2018-01-30T00:00:00"
}
~~~~ 

## Acceptance Criteria

### Lead time added to despatch date  

**Given** an order contains a product from a supplier with a lead time of 1 day  
**And** the order is place on a Monday - 01/01/2018  
**When** the despatch date is calculated  
**Then** the despatch date is Tuesday - 02/01/2018  

**Given** an order contains a product from a supplier with a lead time of 2 days  
**And** the order is place on a Monday - 01/01/2018  
**When** the despatch date is calculated  
**Then** the despatch date is Wednesday - 03/01/2018  

### Supplier with longest lead time is used for calculation

**Given** an order contains a product from a supplier with a lead time of 1 day  
**And** the order also contains a product from a different supplier with a lead time of 2 days  
**And** the order is place on a Monday - 01/01/2018  
**When** the despatch date is calculated  
**Then** the despatch date is Wednesday - 03/01/2018  

### Lead time is not counted over a weekend

**Given** an order contains a product from a supplier with a lead time of 1 day  
**And** the order is place on a Friday - 05/01/2018  
**When** the despatch date is calculated  
**Then** the despatch date is Monday - 08/01/2018  

**Given** an order contains a product from a supplier with a lead time of 1 day  
**And** the order is place on a Saturday - 06/01/18  
**When** the despatch date is calculated  
**Then** the despatch date is Tuesday - 09/01/2018  

**Given** an order contains a product from a supplier with a lead time of 1 days  
**And** the order is place on a Sunday - 07/01/2018  
**When** the despatch date is calculated  
**Then** the despatch date is Tuesday - 09/01/2018  

### Lead time over multiple weeks

**Given** an order contains a product from a supplier with a lead time of 6 days  
**And** the order is place on a Friday - 05/01/2018  
**When** the despatch date is calculated  
**Then** the despatch date is Monday - 15/01/2018  

**Given** an order contains a product from a supplier with a lead time of 11 days  
**And** the order is place on a Friday - 05/01/2018  
**When** the despatch date is calculated  
**Then** the despatch date is Monday - 22/01/2018