defmodule Lms.CatalogTest do
  use LmsWeb.ConnCase

  alias Lms.Catalog

  test "create_course" do
    # Arrange
    expected = %{
      title: "test course",
      description: ["test decription"],
      tagline: "test tagline"
    }

    # Act
    {:ok, actual} = Catalog.create_course(expected)

    # Assert
    assert(expected.title == actual.title)
    assert(expected.tagline == actual.tagline)
    assert(expected.description == actual.description)
  end
end
