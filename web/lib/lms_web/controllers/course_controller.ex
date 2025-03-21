defmodule LmsWeb.CourseController do
  use LmsWeb, :controller
  alias Lms.Catalog
  alias Lms.Catalog.Course
  alias Lms.Repo

  def home(conn, _params) do
    categories = [
      %{title: "sc"},
      %{title: "sc"},
      %{title: "sc"},
      %{title: "sc"},
      %{title: "sc"},
      %{title: "sc"},
      %{title: "[PLACEHOLDER MOCK] category", link: "/test"},
      %{title: "[PLACEHOLDER MOCK] Super Long Category", link: "/test2"}
    ]

    episodes = [
      %{title: "[PLACEHOLDER MOCK] What are things?", description: "Learn what things are."},
      %{title: "[PLACEHOLDER MOCK] Categorizing things", description: "We can arbitrarily place thngs in categories. With luck, we can categorize deliberately."},
      %{title: "[PLACEHOLDER MOCK] Finding things", description: "We lose things sometimes. What do we do then? How do we prevent losing things in the first place!?"},
      %{title: "[PLACEHOLDER MOCK] Examining things", description: "How do we examine things?"}
    ]

    course_mocks = %{
      episodes: episodes,
      categories: categories
    }

    courses = Catalog.list_courses()
    |> Enum.map(fn c -> Map.merge(c, course_mocks) end)

    conn
    |> render(:home, courses: courses)
  end

  def single(conn, %{"id" => id}) do
    categories = [
      %{title: "[PLACEHOLDER MOCK] category", link: "/test"},
      %{title: "[PLACEHOLDER MOCK] Super Long Category", link: "/test2"}
    ]

    episodes = [
      %{title: "[PLACEHOLDER MOCK] What are things?", description: "Learn what things are."},
      %{title: "[PLACEHOLDER MOCK] Categorizing things", description: "We can arbitrarily place thngs in categories. With luck, we can categorize deliberately."},
      %{title: "[PLACEHOLDER MOCK] Finding things", description: "We lose things sometimes. What do we do then? How do we prevent losing things in the first place!?"},
      %{title: "[PLACEHOLDER MOCK] Examining things", description: "How do we examine things?"}
    ]

    course_mocks = %{
      episodes: episodes,
      categories: categories
    }

    Catalog.get_course(id)
    |> case do
      {:ok, course} ->
        conn
        |> render(:single, course: Map.merge(course, course_mocks))
      {:error, "Not Found"} ->
        conn
        |> put_status(404)
        |> render(LmsWeb.ErrorHTML, "404.html")
    end
  end
end
