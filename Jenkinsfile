pipeline {
    agent {
        label 'Windows'
    }

    environment {
        PROJECT_NAME = 'YahooFinanceUI'
        BUILD_DIR = 'dev'
    }

    stages {
        stage('Checkout') {
            steps {
                echo "Checking out repository YahooFinanceUI"
                checkout scm
            }
        }
      stage('Restore') {
            steps {
                echo "Clearing NuGet cache and restoring packages..."
                bat '"C:\\Program Files\\dotnet\\dotnet.exe" nuget locals all --clear'
                bat '"C:\\Program Files\\dotnet\\dotnet.exe" restore YahooFinanceUITests.sln --disable-parallel --verbosity detailed'
            }
        }

        stage('Build') {
            steps {
                echo "Building YahooFinanceUI project..."
                // Replace with your actual build command
                bat '"C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\MSBuild\\Current\\Bin\\MSBuild.exe" YahooFinanceUITests.sln /t:Build /p:Configuration=Debug'
            }
        }

        stage('Test') {
            steps {
                echo "Running UI tests..."
                // Replace with your test command
                sh 'dotnet test YahooFinanceUI.csproj --no-build'
            }
        }

        stage('Archive Artifacts') {
            steps {
                echo "Archiving build artifacts..."
                archiveArtifacts artifacts: "${BUILD_DIR}/**", allowEmptyArchive: false
            }
        }
    }

    post {
        always {
          //  echo "Cleaning up workspace..."
          //  cleanWs()
            echo "Keep build files in ${BUILD_DIR}"
            archiveArtifacts artifacts: '**/', allowEmptyArchive: true
        }
        success {
            echo "Build completed successfully!"
        }
        failure {
            echo "Build failed. Check logs for details."
        }
    }
}