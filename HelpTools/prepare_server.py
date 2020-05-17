import os
import shutil
import subprocess


def create_folders(server_path):
    r_folder = os.path.join(server_path, "Resources")
    os.makedirs(r_folder)

    in_folders = ["db", "downloads", "logs", "models", "uploads"]
    for e in in_folders:
        os.makedirs(os.path.join(r_folder, e))

    add_dependencies(o_folder, os.path.join(r_folder, "models"))


def add_dependencies(server_path, target):
    m_file = "model.sav"
    model_path = os.path.join(server_path, "../algorithm", m_file)
    shutil.move(model_path, os.join.path(target, m_file))


def create_venv(server_path):
    subprocess.Popen(["python", "-m", "venv", os.path.join(server_path, "venv")])
    subprocess.Popen([".\\venv\Scripts\activate"])


if __name__ == "__main__":
    server_path = "../AnalyzeServer/API"

    create_folders(server_path)
    create_venv(server_path)