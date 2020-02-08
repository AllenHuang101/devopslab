/*
 *==========================================================
 * Runs MSTestPublisher
 *
 * call as:
 *  vstestPublisher(
 *      testResultsPattern: '*nunit-result.xml',
 *      currentBuild: currentBuild
 *  )
 *==========================================================
 */

def call(Map parameters = [:]) {

    assert parameters.testResultsPattern != null && parameters.testResultsPattern != 'null' : "testResultsPattern parameter can not be null!"
    assert parameters.currentBuild       != null && parameters.currentBuild       != 'null' : "currentBuild parameter can not be null!"

    println """
The function was called as:
vstestPublisher(
    testResultsPattern: ${parameters.testResultsPattern}
    currentBuild: ${parameters.currentBuild}
)
"""
    step([
        $class: 'MSTestPublisher',
        testResultsFile: parameters.testResultsPattern,
        failOnError: false,
        keepLongStdio: true
    ])
    // feel free to remove it If you don't need
    if(parameters.currentBuild.result == 'UNSTABLE') {
        echo "Build result is ${parameters.currentBuild.result} It'll replace with FAILURE"
        parameters.currentBuild.result = 'FAILURE'
    }
}