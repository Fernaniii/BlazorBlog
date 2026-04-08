# BlazorBlog NUnit Test Checklist

## Overview
This checklist outlines all the tests that should be implemented for the BlazorBlog application using NUnit framework. Tests are organized by layer: Domain, Application, Infrastructure, and UI.

---

## 1. Domain Layer Tests (BlazorBlog.Domain)

### 1.1 Article Entity Tests

#### **Article Creation & Initialization**
- [X] **Test: Create article with valid title and content**
  - Verify Article is created successfully with Create() factory method
  - Assert Title, Content, PublishedOn, and IsPublished values are set correctly
  
- [X] **Test: Create article with null/empty title throws exception**
  - Pass empty string to Create()
  - Assert ArgumentException is thrown
  
- [ ] **Test: Create article with null/empty content throws exception**
  - Pass null or empty string for content
  - Assert ArgumentException is thrown

- [ ] **Test: Create article sets default publication date**
  - Verify PublishedOn is set to current date/time
  
- [ ] **Test: Create article sets IsPublished to false by default**
  - Verify IsPublished property defaults to false

#### **Article Title Management**
- [ ] **Test: SetTitle with valid title updates successfully**
  - Call SetTitle with valid string
  - Assert Title property is updated
  
- [ ] **Test: SetTitle with empty title throws exception**
  - Call SetTitle with empty/null string
  - Assert ArgumentException is thrown with appropriate message

- [ ] **Test: SetTitle with whitespace-only string throws exception**
  - Call SetTitle with whitespace characters only
  - Assert ArgumentException is thrown

#### **Article Content Management**
- [ ] **Test: SetContent with valid content updates successfully**
  - Call SetContent with valid string
  - Assert Content property is updated
  
- [ ] **Test: SetContent with empty content throws exception**
  - Call SetContent with empty/null string
  - Assert ArgumentException is thrown

- [ ] **Test: SetContent with whitespace-only string throws exception**
  - Call SetContent with whitespace characters only
  - Assert ArgumentException is thrown

#### **Article Update**
- [ ] **Test: Update article with valid title and content**
  - Create article and call Update with new values
  - Assert Title and Content are updated
  
- [ ] **Test: Update article with empty title throws exception**
  - Call Update with empty title
  - Assert ArgumentException is thrown
  
- [ ] **Test: Update article with empty content allows update**
  - Call Update with empty content parameter
  - Verify the update completes (if null content is allowed per business logic)

#### **Article Soft Delete Inheritance**
- [ ] **Test: Article inherits SoftDeletableEntity properties**
  - Verify Article has IsDeleted property
  - Verify Article has SoftDelete() method
  
- [ ] **Test: SoftDelete marks article as deleted**
  - Create article and call SoftDelete()
  - Assert IsDeleted is true

---

## 2. Application Layer Tests (BlazorBlog.Application)

### 2.1 ArticleService Tests

#### **Get Operations**
- [X] **Test: GetAllArticlesAsync returns all non-deleted articles**
  - Mock IArticleRepository
  - Setup repository to return list of articles
  - Call GetAllArticlesAsync()
  - Assert returned list matches repository data
  
- [X] **Test: GetAllArticlesAsync returns empty list when no articles exist**
  - Mock repository to return empty list
  - Assert empty list is returned
  
- [ ] **Test: GetByIdAsync returns article for valid ID**
  - Mock repository to return specific article
  - Call GetByIdAsync(1)
  - Assert correct article is returned
  
- [ ] **Test: GetByIdAsync throws exception for non-existent ID**
  - Mock repository to throw KeyNotFoundException
  - Call GetByIdAsync(999)
  - Assert KeyNotFoundException is thrown

#### **Add Operations**
- [ ] **Test: AddAsync adds article successfully**
  - Mock repository
  - Create valid article
  - Call AddAsync()
  - Assert repository.AddAsync was called
  - Assert returned article is valid
  
- [ ] **Test: AddAsync propagates validation errors**
  - Pass invalid article to AddAsync
  - Assert appropriate exception is thrown

#### **Update Operations**
- [ ] **Test: UpdateAsync updates article successfully**
  - Mock repository
  - Create and modify article
  - Call UpdateAsync()
  - Assert repository.UpdateAsync was called with correct article
  
- [ ] **Test: UpdateAsync throws exception for non-existent article**
  - Mock repository to throw exception
  - Assert exception is propagated

#### **Delete Operations**
- [ ] **Test: DeleteAsync deletes article by ID**
  - Mock repository
  - Call DeleteAsync(1)
  - Assert repository.DeleteAsync was called
  - Assert true is returned
  
- [ ] **Test: DeleteAsync returns false for non-existent ID**
  - Mock repository to return false
  - Call DeleteAsync(999)
  - Assert false is returned
  
- [ ] **Test: DeleteAsync propagates exceptions**
  - Mock repository to throw exception
  - Assert exception is propagated correctly

#### **Repository Dependency Injection**
- [ ] **Test: ArticleService requires IArticleRepository dependency**
  - Verify constructor requires non-null IArticleRepository
  - Assert ArgumentNullException if null dependency passed
  
- [ ] **Test: ArticleService uses injected repository**
  - Verify all methods delegate to repository instance

---

## 3. Infrastructure Layer Tests (BlazorBlog.Infrastructure)

### 3.1 ArticleRepository Tests

#### **Database Setup**
- [ ] **Test: ArticleRepository initializes with ApplicationDbContext**
  - Verify constructor accepts DbContext
  - Assert context is stored for use

#### **Add Operations (Database Integration)**
- [X] **Test: AddAsync persists article to database**
  - Use in-memory database
  - Create article
  - Call AddAsync()
  - Query database to verify article exists
  
- [ ] **Test: AddAsync returns the added article**
  - Create article
  - Call AddAsync()
  - Assert returned article has ID assigned
  
- [ ] **Test: AddAsync calls SaveChangesAsync**
  - Mock DbContext
  - Verify SaveChangesAsync is called

#### **Get Operations (Database Integration)**
- [ ] **Test: GetAllArticlesAsync returns all non-deleted articles**
  - Use in-memory database with seed data
  - Add multiple articles (some deleted)
  - Call GetAllArticlesAsync()
  - Assert only non-deleted articles returned
  - Assert deleted articles excluded
  
- [ ] **Test: GetAllArticlesAsync returns empty list when no articles**
  - Use in-memory database
  - Call GetAllArticlesAsync()
  - Assert empty list returned
  
- [ ] **Test: GetByIdAsync retrieves article by ID**
  - Use in-memory database with seed data
  - Call GetByIdAsync(1)
  - Assert correct article returned
  
- [ ] **Test: GetByIdAsync throws KeyNotFoundException for missing article**
  - Use in-memory database
  - Call GetByIdAsync(999)
  - Assert KeyNotFoundException thrown

#### **Update Operations (Database Integration)**
- [ ] **Test: UpdateAsync updates article in database**
  - Use in-memory database with seed data
  - Retrieve article, modify, call UpdateAsync()
  - Query database to verify changes persisted
  
- [ ] **Test: UpdateAsync throws exception for non-existent article**
  - Use in-memory database
  - Create new article with non-existent ID
  - Call UpdateAsync()
  - Assert exception thrown
  
- [ ] **Test: UpdateAsync calls SaveChangesAsync**
  - Mock DbContext
  - Verify SaveChangesAsync is called

#### **Delete Operations (Database Integration)**
- [ ] **Test: DeleteAsync soft-deletes article**
  - Use in-memory database with seed data
  - Call DeleteAsync(1)
  - Query database to verify IsDeleted flag set
  - Assert article still exists in DB but marked deleted
  
- [ ] **Test: DeleteAsync returns false for non-existent article**
  - Use in-memory database
  - Call DeleteAsync(999)
  - Assert false returned
  
- [ ] **Test: DeleteAsync calls SoftDelete()**
  - Mock DbContext
  - Verify article.SoftDelete() is called
  
- [ ] **Test: DeleteAsync calls SaveChangesAsync**
  - Mock DbContext
  - Verify SaveChangesAsync is called

#### **Database Context Integration**
- [ ] **Test: Repository respects soft delete filtering**
  - Seed database with deleted and active articles
  - Verify GetAllArticlesAsync excludes deleted items
  - Verify GetByIdAsync only finds active articles

---

## 4. Integration Tests (Cross-Layer)

### 4.1 Article Domain-to-Application Integration
- [X] **Test: Article entity validation flows through service**
  - Create invalid article
  - Call service method
  - Assert validation error bubbles up correctly

### 4.2 Application-to-Infrastructure Integration
- [ ] **Test: Service calls propagate to repository correctly**
  - Call service method with valid data
  - Verify repository receives correct parameters
  - Verify database state changes

### 4.3 End-to-End Article Operations
- [ ] **Test: Create → Read flow**
  - Create article through service
  - Retrieve it immediately
  - Assert data integrity
  
- [ ] **Test: Create → Update → Read flow**
  - Create article
  - Update it through service
  - Retrieve and verify changes
  
- [ ] **Test: Create → Delete → Verify flow**
  - Create article
  - Delete it through service
  - Verify GetAllArticlesAsync excludes it
  
- [ ] **Test: Multiple concurrent operations**
  - Create multiple articles
  - Update several
  - Delete one
  - Retrieve all
  - Assert consistency

---

## 5. UI/Component Tests (Blazor Components)

### 5.1 Articles.razor Component Tests

#### **Rendering & Initialization**
- [ ] **Test: Articles component renders successfully**
  - Render component
  - Assert no exceptions thrown
  
- [ ] **Test: Articles component loads articles on initialization**
  - Mock IArticleService
  - Setup service to return articles
  - Render component
  - Assert articles displayed

#### **Display Logic**
- [ ] **Test: Component displays article list**
  - Mock service with test articles
  - Render component
  - Assert all articles rendered in UI
  
- [ ] **Test: Component displays empty message when no articles**
  - Mock service to return empty list
  - Render component
  - Assert appropriate message shown
  
- [ ] **Test: Component displays article title**
  - Mock service with article
  - Assert title rendered in output
  
- [ ] **Test: Component displays article content**
  - Mock service with article
  - Assert content rendered in output
  
- [ ] **Test: Component displays publication status**
  - Mock service with published and unpublished articles
  - Assert status displayed correctly

#### **User Interactions**
- [ ] **Test: Delete article triggers deletion**
  - Mock service
  - Render component
  - Trigger delete button
  - Assert service.DeleteAsync called with correct ID
  
- [ ] **Test: Edit article navigates to edit page**
  - Render component
  - Trigger edit action
  - Assert navigation to edit page
  
- [ ] **Test: Create new article button navigation**
  - Render component
  - Click create button
  - Assert navigation to create page

#### **Loading & Error States**
- [ ] **Test: Component shows loading indicator during load**
  - Mock slow service
  - Render component
  - Assert loading state displayed
  
- [ ] **Test: Component handles service errors gracefully**
  - Mock service to throw exception
  - Render component
  - Assert error message displayed
  - Assert component doesn't crash

### 5.2 Home.razor Component Tests

#### **Rendering**
- [ ] **Test: Home component renders successfully**
  - Render component
  - Assert no exceptions
  
- [ ] **Test: Home component displays blog title/header**
  - Render component
  - Assert header content displayed
  
- [ ] **Test: Home component displays featured articles (if applicable)**
  - Mock service
  - Render component
  - Assert featured articles displayed

---

## 6. Edge Cases & Boundary Tests

### 6.1 Data Validation
- [ ] **Test: Maximum length title handling**
  - Create article with very long title
  - Assert handled gracefully
  
- [ ] **Test: Special characters in title/content**
  - Pass HTML, SQL, or special characters
  - Assert handled safely
  
- [ ] **Test: Unicode/International characters**
  - Pass non-English characters
  - Assert processed correctly
  
- [ ] **Test: Very large content**
  - Create article with large content
  - Assert handled without errors

### 6.2 Concurrency
- [ ] **Test: Multiple simultaneous create operations**
  - Run parallel create tasks
  - Assert no conflicts
  
- [ ] **Test: Simultaneous read and write operations**
  - Run concurrent get and update operations
  - Assert data consistency

### 6.3 Null Reference Handling
- [ ] **Test: Null article parameter handling**
  - Pass null to service methods
  - Assert appropriate exception thrown
  
- [ ] **Test: Null repository handling**
  - Service with null repository
  - Assert exception on operation

---

## 7. Test Infrastructure Requirements

### 7.1 Mocking Setup
- [ ] Setup Moq for mock objects
- [ ] Create mock builders for common objects:
  - Mock IArticleRepository
  - Mock IArticleService
  - Mock ApplicationDbContext
  - Mock Articles DbSet

### 7.2 Fixtures & Test Data
- [ ] Create ArticleTestFixture with sample data
- [ ] Create builder pattern for Article test data:
  - Default valid article
  - Article with minimal data
  - Deleted article
  
### 7.3 In-Memory Database Setup
- [ ] Configure InMemory database for integration tests
- [ ] Create database seeding utilities
- [ ] Create DbContext factory for tests

### 7.4 Async Test Patterns
- [ ] Setup async task testing patterns
- [ ] Configure Task completion verification
- [ ] Setup timeout handling for async tests

---

## 8. Test Coverage Goals

- **Domain Layer**: 95%+ code coverage
  - Critical: Entity validation logic
  - Critical: Business rule enforcement
  
- **Application Layer**: 90%+ code coverage
  - Critical: Service method delegation
  - Critical: Exception handling
  
- **Infrastructure Layer**: 85%+ code coverage
  - Critical: Database persistence
  - Critical: CRUD operations
  
- **UI Components**: 70%+ coverage
  - Critical: Main user workflows
  - Critical: Error handling

---

## 9. Testing Best Practices

### 9.1 Test Naming
- [ ] Use descriptive test names following pattern: `Should_[Expected]_When_[Condition]`
- [ ] Example: `Should_ThrowArgumentException_When_TitleIsEmpty`

### 9.2 Arrange-Act-Assert Pattern
- [ ] All tests follow AAA pattern
- [ ] Clear separation of setup, execution, verification

### 9.3 Test Isolation
- [ ] Each test is independent
- [ ] No shared state between tests
- [ ] Setup and cleanup properly handled

### 9.4 Assertions
- [ ] Use specific assertions (Assert.That with Throws, Is.EqualTo, etc.)
- [ ] Multiple assertions only when testing related outcomes
- [ ] Clear assertion messages for failures

---

## 10. Continuous Integration

- [ ] All tests run in CI pipeline
- [ ] Tests must pass before merge
- [ ] Code coverage reports generated
- [ ] Test results tracked over time

---

## Notes

- Priority: Start with Domain Layer tests (Article entity)
- Then: Application Layer (ArticleService)
- Then: Infrastructure Layer (ArticleRepository with in-memory DB)
- Finally: UI Components and Integration tests
- Use Moq for dependency injection and mocking
- Use NUnit framework for test structure and assertions

