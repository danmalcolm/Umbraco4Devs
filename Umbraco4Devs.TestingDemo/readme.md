#Umbraco4Devs - Automated Testing Demo

## Install Umbraco Web Application


## Introducing our Demo Application - The uBlog Platform!

We're building a site that allows members to host their own blog. Each member gets a blog, to which they can add posts.

So, we'll start by adding some simple document types.

Home Page
The Home Page contains one or more Blogs
Each Blog contains one or more Post Posts
Registration Page

We create a Blogger MemberType, which has a Blog referencing the blog that the member has access to

Things we might want to test:

* Registration functionality - is account created? Is blog created for user?
* Blog post creation and update

## When Testing?



## Basic Content Configuration

Good automated tests are repeatable and don't rely extensively on existing data or content.

However, we're going to assume that any aspects of the content structure will already be set up when the tests run. When it comes to the content tree, we have to think carefully and strike a balance.


This includes things like document types, media types, member types etc, anything to do with Umbraco's content structure.



## Add a Class Library for Tests

### Add class library


### Install Umbraco

Install UmbracoCms.Core package:

> install-package UmbracoCms.Core

### Update app.config

Update app.config with settings from web application's web.config file.

We then update the connection string within the app.config file to match the database running the web application.

### Copy Configuration

Set content action of all config files to "Content". This ensures that they get copied to the base directory bin/Debug or bin/Release

### Add Supporting Application Classes

Based on StandaloneCoreApplication from Umbraco source.

### Add Custom SiteMapProvider

Configure in app.config file

### Examine

Umbraco will recreate indexes upon startup. I spent about 2 hours attempting to reindex, then noticed that if there is a sole home document, the ExternalIndex doesn't seem to index the home document and the index is empty. Bear this in mind and save yourself 2 hours.

## Start Writing Some Tests

### Getting Umbraco Running outside of a Web Application

First test is extremely simple -




## What Works?



## What Doesn't Work?

E.g. testing member registration - does it try to use FormsAuthentication?


## Cleaning Up In Between Tests


## Adding a Dedicated Test Database

We started off by working with the same database that we managed via the web application, just to get started.

However, if we're regularly running tests, deleting and adding test data, it isn't a good idea to share the development database.

So we make a copy (via backup / restore) - we now have a database we can work with.


## Keeping the Dedicated Test Database in Sync

As our application evolves, the content structure will change. New document types and key nodes within the content tree will be added. In the last step we started working with a separate database. So we need to think about keeping this up to date.

Options include:

* Having a setup routine that creates everything from scratch
+ Accurate and repeatable, you can start with a blank database
- Discipline needed
- Hard work, setting up things like icons etc, descriptions etc
- 2 mechanisms used - code used in tests, back office by developer

* Periodic Refresh of test database
+ No repetition and fits in with typical process
+ Manual and clunky - are developers going to have discipline to keep things in sync?

* Package Import
+ Clean way of doing things, useful rehearsal
+ Can be automated
- Creating the package is manual - not that bad though...
