﻿// Colten Mayberry

using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class BlogManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        // Provides the main menu for the user to select an option.
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List Blogs");
            Console.WriteLine(" 2) Blog Details");
            Console.WriteLine(" 3) Add Blog");
            Console.WriteLine(" 4) Edit Blog");
            Console.WriteLine(" 5) Remove Blog");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    throw new NotImplementedException();
                case "3":
                    Add();
                    return this;
                case "4":
                    Edit();
                    return this;
                case "5":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        // The list method.  Lists all blogs.
        private void List()
        {
            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                // Returns title and url due to overridden method
                Console.WriteLine(blog);
            }
        }

        //? What does this do?
        private Blog Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Blog:";
            }

            Console.WriteLine(prompt);

            List<Blog> blogs = _blogRepository.GetAll();

            for (int i = 0; i < blogs.Count; i++)
            {
                Blog blog = blogs[i];
                Console.WriteLine($" {i + 1} {blog.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return blogs[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        // The Add method.  Adds a blog to the blog table with user entered title and url.
        private void Add()
        {
            Console.WriteLine("New Blog");
            Blog blog = new Blog();

            Console.Write("Title: ");
            blog.Title = Console.ReadLine();

            Console.Write("URL: ");
            blog.Url = Console.ReadLine();

            _blogRepository.Insert(blog);
        }

        // The Edit method.  Adds edits a single blog on the blog table with the user defined changes.  Note that it utilizes the Choose method.
        private void Edit()
        {
            Blog blogToEdit = Choose("Which blog would you like to edit?");
            if (blogToEdit == null)
            {
                return;
            }
            Console.WriteLine();
            Console.Write("New title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                blogToEdit.Title = title;
            }
            Console.Write("New URL (blank to leave unchaged: ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                blogToEdit.Url = url;
            }
            _blogRepository.Update(blogToEdit);
        }

        // The Remove method.  Note that it utilizes the Choose method.
        private void Remove()
        {
            Blog blogToDelete = Choose("Which blog would you like to remove?");
            if (blogToDelete != null)
            {
                _blogRepository.Delete(blogToDelete.Id);
            }
        }
    }
}
