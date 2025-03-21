defmodule LmsWeb.Router do
  use LmsWeb, :router
  use Kaffy.Routes, scope: "/admin"

  pipeline :browser do
    plug :accepts, ["html"]
    plug :fetch_session
    plug :put_root_layout, html: {LmsWeb.Layouts, :root}
    plug :protect_from_forgery
    plug :put_secure_browser_headers
  end

  pipeline :api do
    plug :accepts, ["json"]
  end

  scope "/", LmsWeb do
    pipe_through :browser

    get "/", PageController, :home
  end

  scope "/courses", LmsWeb do
    pipe_through :browser

    get "/", CourseController, :home
    get "/:id", CourseController, :single
  end

  if Application.compile_env(:lms, :dev_routes) do
    scope "/dev" do
      pipe_through :browser

      forward "/mailbox", Plug.Swoosh.MailboxPreview
    end
  end
end
