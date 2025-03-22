defmodule LmsWeb.CourseControllerTest do
  use LmsWeb.ConnCase
  alias Lms.Catalog

  defp assert_course_data_in_home(actual, course) do
    assert actual =~ course.title
    assert actual =~ course.tagline
  end

  test "render_home", %{conn: conn} do
    # Arrange
    course_creations = 1..5
    |> Enum.map(fn x -> %{
      title: "Course #{x}",
      description: ["Description #{x}"],
      tagline: "Tagline #{x}"
      }
    end)
    |> Enum.map(fn x ->
      Catalog.create_course(x)
      |> case do
        {:ok, course} -> course
      end
    end)

    # Act
    actual = conn
    |> get("/courses")
    |> html_response(200)

    # Assert
    course_creations
    |> Enum.map(fn course -> assert_course_data_in_home(actual, course) end)
  end

  test "render_single", %{conn: conn} do
    # Arrange
    expected = %{
      title: "test course",
      description: ["test decription"],
      tagline: "test tagline"
    }

    # Act
    {:ok, created} = Catalog.create_course(expected)
    conn = get(conn, "/courses/#{created.id}")

    # Assert
    assert html_response(conn, 200) =~ expected.title
  end
end
