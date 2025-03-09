defmodule LmsWeb.PageController do
  use LmsWeb, :controller

  def home(conn, _params) do
    # The home page is often custom made,
    # so skip the default app layout.
    conn
    |> put_flash(:info, "Welcome")
    |> render(:home, layout: false)
  end
end
