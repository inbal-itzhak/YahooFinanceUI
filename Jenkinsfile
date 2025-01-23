pipeline {
    agent {
        label 'Built-In Node'
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

        stage('Build') {
            steps {
                echo "Building YahooFinanceUI project..."
                // Replace with your actual build command
                sh 'dotnet build YahooFinanceUITests.sln --configuration Debug'
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
