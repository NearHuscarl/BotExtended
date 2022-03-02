import util from "util";
import child_process from "child_process";
import chalk from "chalk";

const exec = util.promisify(child_process.exec);

async function getStagedModifiedFiles() {
  const { stdout } = await exec(
    "git diff --cached --name-only --diff-filter=M"
  );
  return stdout.split("\n").filter(Boolean);
}

const main = async () => {
  console.log("run commit-msg hook");
  const commitMsg = process.argv[2];
  const stagedModifiedFiles = await getStagedModifiedFiles();

  if (
    !commitMsg.startsWith("[docs] ") &&
    stagedModifiedFiles.includes("README.md")
  ) {
    console.error(
      chalk.red(
        'Commit message must start with "[docs]" when README.md is modified'
      )
    );
    process.exit(1);
  }
};

main();
