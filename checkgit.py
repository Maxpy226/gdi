import git
import time
import os

repo_path = os.getcwd() + "\gdi" # Change this to your local repo folder
branch_name = "main"  # Change of your branch is different

repo = git.Repo(repo_path)
local_branch = repo.heads[branch_name]
remote_branch = repo.refs[f'origin/{branch_name}']

def commits_behind(local, remote):
    return list(remote.commit.iter_items(repo, f'{local.commit.hexsha}..{remote.commit.hexsha}'))

while True:
    print("Fetching remote changes...")
    repo.remote().fetch()

    behind = commits_behind(local_branch, remote_branch)

    if behind:
        print(f"{len(behind)} new commits found on remote. Pulling changes...")
        repo.remotes.origin.pull(branch_name)
        print("Pull complete.")
    else:
        print("No new commits. Local repo is up-to-date.")

    time.sleep(10)  # Wait 10 seconds before checking again
