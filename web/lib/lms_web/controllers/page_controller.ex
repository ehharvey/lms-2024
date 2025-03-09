defmodule LmsWeb.PageController do
  use LmsWeb, :controller

  def home(conn, _params) do
    conn
    |> render(:home)
  end
end
