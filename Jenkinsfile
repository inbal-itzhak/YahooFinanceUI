pipeline {
    agent {
        label 'Windows'
    }

    environment {
        PROJECT_NAME = 'YahooFinanceUI'
        BUILD_DIR = 'bin/Debug/net8.0'
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
                echo "Restoring NuGet packages..."
                bat '"C:\\Program Files\\dotnet\\dotnet.exe" restore YahooFinanceUITests.sln'
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
            echo "Cleaning up workspace..."
            cleanWs()
        }
        success {
            echo "Build completed successfully!"
        }
        failure {
            echo "Build failed. Check logs for details."
        }
    }
}
