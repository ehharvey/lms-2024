FROM mcr.microsoft.com/devcontainers/base:bookworm

RUN apt update
RUN apt upgrade -y
RUN apt install -y build-essential autoconf automake m4 libncurses-dev libwxgtk3.2-dev libwxgtk-webview3.2-dev libgl1-mesa-dev libglu1-mesa-dev libpng-dev libssh-dev unixodbc-dev xsltproc fop libxml2-utils libncurses-dev openjdk-17-jdk

RUN mkdir -p /opt/asdf/bin

RUN echo "export PATH=\$PATH:/opt/asdf/bin" >> /etc/profile.d/asdf.sh
RUN echo "export PATH=\${ASDF_DATA_DIR:-\$HOME/.asdf}/shims:\$PATH" >> /etc/profile.d/asdf.sh
RUN chmod 755 /etc/profile.d/asdf.sh

RUN wget https://github.com/asdf-vm/asdf/releases/download/v0.16.4/asdf-v0.16.4-linux-amd64.tar.gz -O /opt/asdf/bin/asdf.tar.gz
RUN cd /opt/asdf/bin/ && tar -xvf asdf.tar.gz

RUN /opt/asdf/bin/asdf completion bash >> /etc/bash_completion.d/asdf

RUN su vscode -c '/opt/asdf/bin/asdf plugin add erlang https://github.com/asdf-vm/asdf-erlang.git'
RUN su vscode -c '/opt/asdf/bin/asdf install erlang 27.2.4'
RUN su vscode -c '/opt/asdf/bin/asdf set -u erlang 27.2.4'

RUN su vscode -c '/opt/asdf/bin/asdf plugin add elixir https://github.com/asdf-vm/asdf-elixir.git'
RUN su vscode -c '/opt/asdf/bin/asdf install elixir 1.18.2'
RUN su vscode -c '/opt/asdf/bin/asdf set -u elixir 1.18.2'