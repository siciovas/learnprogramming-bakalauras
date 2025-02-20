/* eslint-disable no-undef */
var testCase;
var testResult = "";
export const outf = (text) => {
  var mypre = document.getElementById("output");
  mypre.value = mypre.value + text;
};

function stdinInput(prompt) {
  return new Promise((resolve, reject) => {
    resolve(testCase);
  });
}

function stdoutOutput(text) {
    testResult = testResult + text;
}

function callPrompt(args) {
  return window.prompt(args);
}

export const run = async (code) => {
  var mypre = document.getElementById("output");
  mypre.value = "";
  Sk.pre = "output";
  Sk.configure({
    inputfun: callPrompt,
    inputfunTakesPrompt: true,
    output: outf,
  });
  await Sk.misceval
    .asyncToPromise(function () {
      return Sk.importMainWithBody("<stdin>", false, code, true);
    })
    .then(
      function (result) {},
      function (err) {
        throw new Error(err.toString());
      }
    );
};

export const runTest = async (code, test) => {
  testResult = "";
  testCase = test;
  Sk.configure({
    inputfun: stdinInput,
    inputfunTakesPrompt: true,
    output: stdoutOutput,
  });
  await Sk.misceval
    .asyncToPromise(function () {
      return Sk.importMainWithBody("<stdin>", false, code, true);
    })
    .then(
      function (result) {},
      function (err) {
        throw new Error(err.toString());
      }
    );
  return testResult;
};
